﻿// 
// SharedStringCache.cs
// 
// Copyright (c) 2017 Couchbase, Inc All rights reserved.
// 
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
// 
// http://www.apache.org/licenses/LICENSE-2.0
// 
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
// 
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

using Couchbase.Lite.Interop;

using LiteCore.Interop;

namespace Couchbase.Lite.Internal.Serialization
{
    internal sealed unsafe class SharedStringCache
    {
        #region Variables

        private readonly IDictionary<int, string> _documentStrings = new Dictionary<int, string>();
        private FLDict* _root;

        #endregion

        public FLSharedKeys* SharedKeys { get; }

        #region Constructors

        public SharedStringCache()
        {
            
        }

        public SharedStringCache(FLSharedKeys* keys)
        {
            Debug.Assert(keys != null);
            SharedKeys = keys;
        }

        public SharedStringCache(SharedStringCache other)
        {
            Debug.Assert(other != null);
            SharedKeys = other.SharedKeys;
        }

        public SharedStringCache(SharedStringCache other, FLDict* root)
            : this(other)
        {
            _root = root;
        }

        #endregion

        #region Public Methods

        public string GetDictIterKey(FLDictIterator* iter)
        {
            var key = Native.FLDictIterator_GetKey(iter);
            if (key == null) {
                return null;
            }

            return Native.FLValue_IsInteger(key) ? GetKey((int)Native.FLValue_AsInt(key)) : Native.FLValue_AsString(key);
        }

        public FLValue* GetDictValue(FLDict* dict, string key)
        {
            return Native.FLDict_GetSharedKey(dict, Encoding.UTF8.GetBytes(key), SharedKeys);
        }

        public FLValue* GetDictValue(FLDict* dict, FLSlice key)
        {
            return NativeRaw.FLDict_GetSharedKey(dict, key, SharedKeys);
        }

        public string GetKey(int index)
        {
            string retVal;
            if(_documentStrings.TryGetValue(index, out retVal)) {
                return retVal;
            }

            retVal = Native.FLSharedKey_GetKeyString(SharedKeys, index, null);
            if(retVal != null) {
                _documentStrings[index] = retVal;
            }

            return retVal;
        }

        #endregion

        #region Internal Methods

        internal void UseDocumentRoot(FLDict* root)
        {
            if (_root != root) {
                _root = root;
                _documentStrings.Clear();
            }
        }

        #endregion
    }
}
