﻿// 
//  ILiteCore.cs
// 
//  Copyright (c) 2018 Couchbase, Inc All rights reserved.
// 
//  Licensed under the Apache License, Version 2.0 (the "License");
//  you may not use this file except in compliance with the License.
//  You may obtain a copy of the License at
// 
//  http://www.apache.org/licenses/LICENSE-2.0
// 
//  Unless required by applicable law or agreed to in writing, software
//  distributed under the License is distributed on an "AS IS" BASIS,
//  WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
//  See the License for the specific language governing permissions and
//  limitations under the License.
// 
using System;

using Couchbase.Lite.Interop;

namespace LiteCore.Interop
{
    internal unsafe partial interface ILiteCore
    {
        bool c4SliceEqual(C4Slice a, C4Slice b);
        void c4slice_free(C4SliceResult slice);
        string c4error_getMessage(C4Error error);
        C4Error c4error_make(C4ErrorDomain domain, int code, string message);
        bool c4error_mayBeTransient(C4Error err);
        bool c4error_mayBeNetworkDependent(C4Error err);
        void c4log_writeToCallback(C4LogLevel level, C4LogCallback callback, bool preformatted);
        bool c4log_writeToBinaryFile(C4LogLevel level, string path, C4Error* error);
        C4LogLevel c4log_callbackLevel();
        void c4log_setCallbackLevel(C4LogLevel level);
        C4LogLevel c4log_binaryFileLevel();
        void c4log_setBinaryFileLevel(C4LogLevel level);
        string c4log_getDomainName(C4LogDomain* x);
        C4LogLevel c4log_getLevel(C4LogDomain* x);
        void c4log_setLevel(C4LogDomain* c4Domain, C4LogLevel level);
        void c4slog(C4LogDomain* domain, C4LogLevel level, string msg);
        string c4_getBuildInfo();
        string c4_getVersion();
        int c4_getObjectCount();
        void c4_dumpInstances();
        bool c4blob_keyFromString(string str, C4BlobKey* x);
        string c4blob_keyToString(C4BlobKey key);
        C4BlobStore* c4db_getBlobStore(C4Database* db, C4Error* outError);
        C4BlobStore* c4blob_openStore(string dirPath, C4DatabaseFlags flags, C4EncryptionKey* encryptionKey, C4Error* outError);
        void c4blob_freeStore(C4BlobStore* store);
        bool c4blob_deleteStore(C4BlobStore* store, C4Error* outError);
        long c4blob_getSize(C4BlobStore* store, C4BlobKey key);
        byte[] c4blob_getContents(C4BlobStore* store, C4BlobKey key, C4Error* outError);
        string c4blob_getFilePath(C4BlobStore* store, C4BlobKey key, C4Error* outError);
        C4BlobKey c4blob_computeKey(byte[] contents);
        bool c4blob_create(C4BlobStore* store, byte[] contents, C4BlobKey* expectedKey, C4BlobKey* outKey, C4Error* error);
        bool c4blob_delete(C4BlobStore* store, C4BlobKey key, C4Error* outError);
        C4ReadStream* c4blob_openReadStream(C4BlobStore* store, C4BlobKey key, C4Error* outError);
        ulong c4stream_read(C4ReadStream *stream, byte[] buffer, int count, C4Error *outError);
        long c4stream_getLength(C4ReadStream* stream, C4Error* outError);
        bool c4stream_seek(C4ReadStream* stream, ulong position, C4Error* outError);
        void c4stream_close(C4ReadStream* stream);
        C4WriteStream* c4blob_openWriteStream(C4BlobStore* store, C4Error* outError);
        bool c4stream_write(C4WriteStream* stream, byte[] bytes, ulong length, C4Error* outError);
        C4BlobKey c4stream_computeBlobKey(C4WriteStream* stream);
        bool c4stream_install(C4WriteStream* stream, C4BlobKey* expectedKey, C4Error* outError);
        void c4stream_closeWriter(C4WriteStream* stream);
        C4Database* c4db_open(string path, C4DatabaseConfig* config, C4Error* outError);
        C4Database* c4db_openAgain(C4Database* db, C4Error* outError);
        bool c4db_copy(string sourcePath, string destinationPath, C4DatabaseConfig* config, C4Error* error);
        C4Database* c4db_retain(C4Database* db);
        bool c4db_free(C4Database* database);
        bool c4db_close(C4Database* database, C4Error* outError);
        bool c4db_delete(C4Database* database, C4Error* outError);
        bool c4db_deleteAtPath(string dbPath, C4Error* outError);
        bool c4db_rekey(C4Database* database, C4EncryptionKey* newKey, C4Error* outError);
        bool c4_shutdown(C4Error* outError);
        string c4db_getPath(C4Database* db);
        C4DatabaseConfig* c4db_getConfig(C4Database* db);
        ulong c4db_getDocumentCount(C4Database* database);
        ulong c4db_getLastSequence(C4Database* database);
        ulong c4db_nextDocExpiration(C4Database* database);
        uint c4db_getMaxRevTreeDepth(C4Database* database);
        void c4db_setMaxRevTreeDepth(C4Database* database, uint maxRevTreeDepth);
        bool c4db_getUUIDs(C4Database* database, C4UUID* publicUUID, C4UUID* privateUUID, C4Error* outError);
        bool c4db_compact(C4Database* database, C4Error* outError);
        bool c4db_beginTransaction(C4Database* database, C4Error* outError);
        bool c4db_endTransaction(C4Database* database, bool commit, C4Error* outError);
        bool c4db_isInTransaction(C4Database* database);
        void c4raw_free(C4RawDocument* rawDoc);
        C4RawDocument* c4raw_get(C4Database* database, string storeName, string docID, C4Error* outError);
        bool c4raw_put(C4Database* database, string storeName, string key, string meta, string body, C4Error* outError);
        void c4enum_close(C4DocEnumerator* e);
        void c4enum_free(C4DocEnumerator* e);
        C4DocEnumerator* c4db_enumerateChanges(C4Database* database, ulong since, C4EnumeratorOptions* options, C4Error* outError);
        C4DocEnumerator* c4db_enumerateAllDocs(C4Database* database, C4EnumeratorOptions* options, C4Error* outError);
        bool c4enum_next(C4DocEnumerator* e, C4Error* outError);
        C4Document* c4enum_getDocument(C4DocEnumerator* e, C4Error* outError);
        bool c4enum_getDocumentInfo(C4DocEnumerator* e, C4DocumentInfo* outInfo);
        bool c4doc_isOldMetaProperty(string prop);
        bool c4doc_hasOldMetaProperties(FLDict* doc);
        byte[] c4doc_encodeStrippingOldMetaProperties(FLDict* doc);
        bool c4doc_dictIsBlob(FLDict* dict, FLSharedKeys* sk, C4BlobKey* outKey);
        bool c4doc_dictContainsBlobs(FLDict* dict, FLSharedKeys* sk);
        bool c4doc_blobIsCompressible(FLDict* blobDict, FLSharedKeys* sk);
        string c4doc_bodyAsJSON(C4Document* doc, bool canonical, C4Error* outError);
        FLEncoder* c4db_createFleeceEncoder(C4Database* db);
        FLEncoder* c4db_getSharedFleeceEncoder(C4Database* db);
        byte[] c4db_encodeJSON(C4Database* db, string jsonData, C4Error* outError);
        FLSharedKeys* c4db_getFLSharedKeys(C4Database* db);
        FLDictKey c4db_initFLDictKey(C4Database* db, string str);
        C4Document* c4doc_get(C4Database* database, string docID, bool mustExist, C4Error* outError);
        C4Document* c4doc_getBySequence(C4Database* database, ulong sequence, C4Error* outError);
        bool c4doc_save(C4Document* doc, uint maxRevTreeDepth, C4Error* outError);
        void c4doc_free(C4Document* doc);
        bool c4doc_selectRevision(C4Document* doc, string revID, bool withBody, C4Error* outError);
        bool c4doc_selectCurrentRevision(C4Document* doc);
        bool c4doc_loadRevisionBody(C4Document* doc, C4Error* outError);
        string c4doc_detachRevisionBody(C4Document* doc);
        bool c4doc_hasRevisionBody(C4Document* doc);
        bool c4doc_selectParentRevision(C4Document* doc);
        bool c4doc_selectNextRevision(C4Document* doc);
        bool c4doc_selectNextLeafRevision(C4Document* doc, bool includeDeleted, bool withBody, C4Error* outError);
        bool c4doc_selectFirstPossibleAncestorOf(C4Document* doc, string revID);
        bool c4doc_selectNextPossibleAncestorOf(C4Document* doc, string revID);
        bool c4doc_selectCommonAncestorRevision(C4Document* doc, string rev1ID, string rev2ID);
        uint c4db_getRemoteDBID(C4Database* db, string remoteAddress, bool canCreate, C4Error* outError);
        byte[] c4db_getRemoteDBAddress(C4Database* db, uint remoteID);
        byte[] c4doc_getRemoteAncestor(C4Document* doc, uint remoteDatabase);
        bool c4doc_setRemoteAncestor(C4Document* doc, uint remoteDatabase, C4Error* error);
        uint c4rev_getGeneration(string revID);
        bool c4doc_removeRevisionBody(C4Document* doc);
        int c4doc_purgeRevision(C4Document* doc, string revID, C4Error* outError);
        bool c4doc_resolveConflict(C4Document* doc, string winningRevID, string losingRevID, byte[] mergedBody, C4RevisionFlags mergedFlags, C4Error* error);
        bool c4db_purgeDoc(C4Database* database, string docID, C4Error* outError);
        bool c4doc_setExpiration(C4Database* db, string docId, ulong timestamp, C4Error* outError);
        ulong c4doc_getExpiration(C4Database* db, string docId);
        C4Document* c4doc_put(C4Database *database, C4DocPutRequest *request, ulong* outCommonAncestorIndex, C4Error *outError);
        C4Document* c4doc_create(C4Database* db, string docID, byte[] body, C4RevisionFlags revisionFlags, C4Error* error);
        C4Document* c4doc_update(C4Document* doc, byte[] revisionBody, C4RevisionFlags revisionFlags, C4Error* error);
        C4ExpiryEnumerator* c4db_enumerateExpired(C4Database* database, C4Error* outError);
        bool c4exp_next(C4ExpiryEnumerator* e, C4Error* outError);
        string c4exp_getDocID(C4ExpiryEnumerator* e);
        bool c4exp_purgeExpired(C4ExpiryEnumerator* e, C4Error* outError);
        void c4exp_close(C4ExpiryEnumerator* e);
        void c4exp_free(C4ExpiryEnumerator* e);
        C4DatabaseObserver* c4dbobs_create(C4Database* database, C4DatabaseObserverCallback callback, void* context);
        uint c4dbobs_getChanges(C4DatabaseObserver* observer, C4DatabaseChange[] outChanges, uint maxChanges, bool* outExternal);
        void c4dbobs_releaseChanges(C4DatabaseChange[] changes, uint numChanges);
        void c4dbobs_free(C4DatabaseObserver* observer);
        C4DocumentObserver* c4docobs_create(C4Database* database, string docID, C4DocumentObserverCallback callback, void* context);
        void c4docobs_free(C4DocumentObserver* observer);
        C4Query* c4query_new(C4Database* database, string expression, C4Error* error);
        void c4query_free(C4Query* x);
        string c4query_explain(C4Query* query);
        uint c4query_columnCount(C4Query* query);
        C4QueryEnumerator* c4query_run(C4Query* query, C4QueryOptions* options, string encodedParameters, C4Error* outError);
        string c4query_fullTextMatched(C4Query* query, C4FullTextMatch* term, C4Error* outError);
        bool c4queryenum_next(C4QueryEnumerator* e, C4Error* outError);
        long c4queryenum_getRowCount(C4QueryEnumerator* e, C4Error* outError);
        bool c4queryenum_seek(C4QueryEnumerator* e, ulong rowIndex, C4Error* outError);
        C4QueryEnumerator* c4queryenum_refresh(C4QueryEnumerator* e, C4Error* outError);
        void c4queryenum_close(C4QueryEnumerator* e);
        void c4queryenum_free(C4QueryEnumerator* e);
        bool c4db_createIndex(C4Database* database, string name, string expressionsJSON, C4IndexType indexType, C4IndexOptions* indexOptions, C4Error* outError);
        bool c4db_deleteIndex(C4Database* database, string name, C4Error* outError);
        byte[] c4db_getIndexes(C4Database* database, C4Error* outError);
        bool c4repl_isValidDatabaseName(string dbName);
        bool c4address_fromURL(string url, C4Address* address, C4Slice* dbName);
        string c4address_toURL(C4Address address);
        C4Replicator* c4repl_new(C4Database* db, C4Address remoteAddress, C4Slice remoteDatabaseName, C4Database* otherLocalDB, C4ReplicatorParameters @params, C4Error* outError);
        C4Replicator* c4repl_newWithSocket(C4Database* db, C4Socket* openSocket, C4ReplicatorParameters @params, C4Error* outError);
        void c4repl_free(C4Replicator* repl);
        void c4repl_stop(C4Replicator* repl);
        C4ReplicatorStatus c4repl_getStatus(C4Replicator* repl);
        C4Slice c4repl_getResponseHeaders(C4Replicator* repl);
        bool c4db_setCookie(C4Database* db, string setCookieHeader, string fromHost, string fromPath, C4Error* outError);
        string c4db_getCookies(C4Database* db, C4Address request, C4Error* error);
        void c4db_clearCookies(C4Database* db);
        void c4socket_registerFactory(C4SocketFactory factory);
        void c4socket_gotHTTPResponse(C4Socket* socket, int httpStatus, C4Slice responseHeadersFleece);
        void c4socket_opened(C4Socket* socket);
        void c4socket_closed(C4Socket* socket, C4Error errorIfAny);
        void c4socket_closeRequested(C4Socket* socket, int status, string message);
        void c4socket_completedWrite(C4Socket* socket, ulong byteCount);
        void c4socket_received(C4Socket* socket, byte[] data);
        C4Socket* c4socket_fromNative(C4SocketFactory factory, void* nativeHandle, C4Address* address);
        void FLSliceResult_Free(FLSliceResult slice);
        bool FLSlice_Equal(FLSlice a, FLSlice b);
        int FLSlice_Compare(FLSlice left, FLSlice right);
        FLValue* FLValue_FromData(byte[] data);
        FLValue* FLValue_FromTrustedData(byte[] data);
        byte[] FLData_ConvertJSON(byte[] json, FLError* outError);
        string FLData_Dump(FLSlice data);
        FLValueType FLValue_GetType(FLValue* value);
        bool FLValue_IsInteger(FLValue* value);
        bool FLValue_IsUnsigned(FLValue* value);
        bool FLValue_IsDouble(FLValue* value);
        bool FLValue_AsBool(FLValue* value);
        long FLValue_AsInt(FLValue* value);
        ulong FLValue_AsUnsigned(FLValue* value);
        float FLValue_AsFloat(FLValue* value);
        double FLValue_AsDouble(FLValue* value);
        string FLValue_AsString(FLValue* value);
        byte[] FLValue_AsData(FLValue* value);
        FLArray* FLValue_AsArray(FLValue* value);
        FLDict* FLValue_AsDict(FLValue* value);
        string FLValue_ToString(FLValue* value);
        string FLValue_ToJSON(FLValue* value);
        string FLValue_ToJSON5(FLValue* v);
        string FLValue_ToJSONX(FLValue* v, FLSharedKeys* sk, bool json5, bool canonicalForm);
        string FLJSON5_ToJSON(string json5, FLError* error);
        uint FLArray_Count(FLArray* array);
        bool FLArray_IsEmpty(FLArray* array);
        FLValue* FLArray_Get(FLArray* array, uint index);
        void FLArrayIterator_Begin(FLArray* array, FLArrayIterator* i);
        FLValue* FLArrayIterator_GetValue(FLArrayIterator* i);
        FLValue* FLArrayIterator_GetValueAt(FLArrayIterator* i, uint offset);
        uint FLArrayIterator_GetCount(FLArrayIterator* i);
        bool FLArrayIterator_Next(FLArrayIterator* i);
        uint FLDict_Count(FLDict* dict);
        bool FLDict_IsEmpty(FLDict* dict);
        FLValue* FLDict_Get(FLDict* dict, byte[] keyString);
        FLValue* FLDict_GetSharedKey(FLDict* d, byte[] keyString, FLSharedKeys* sk);
        string FLSharedKey_GetKeyString(FLSharedKeys* sk, int keyCode, FLError* outError);
        FLValue* FLDict_GetUnsorted(FLDict* dict, byte[] keyString);
        void FLDictIterator_Begin(FLDict* dict, FLDictIterator* i);
        void FLDictIterator_BeginShared(FLDict* dict, FLDictIterator* i, FLSharedKeys* shared);
        FLValue* FLDictIterator_GetKey(FLDictIterator* i);
        string FLDictIterator_GetKeyString(FLDictIterator* i);
        FLValue* FLDictIterator_GetValue(FLDictIterator* i);
        uint FLDictIterator_GetCount(FLDictIterator* i);
        bool FLDictIterator_Next(FLDictIterator* i);
        FLDictKey FLDictKey_Init(string str, bool cachePointers);
        FLDictKey FLDictKey_InitWithSharedKeys(string str, FLSharedKeys* sk);
        string FLDictKey_GetString(FLDictKey* key);
        FLValue* FLDict_GetWithKey(FLDict* dict, FLDictKey* dictKey);
        ulong FLDict_GetWithKeys(FLDict* dict, FLDictKey[] keys, FLValue[] values, ulong count);
        FLKeyPath* FLKeyPath_New(byte[] specifier, FLSharedKeys* shared, FLError* error);
        void FLKeyPath_Free(FLKeyPath* keyPath);
        FLValue* FLKeyPath_Eval(FLKeyPath* keyPath, FLValue* root);
        FLValue* FLKeyPath_EvalOnce(byte[] specifier, FLSharedKeys* shared, FLValue* root, FLError* error);
        FLEncoder* FLEncoder_New();
        FLEncoder* FLEncoder_NewWithOptions(FLEncoderFormat format, ulong reserveSize, bool uniqueStrings, bool sortKeys);
        void FLEncoder_Free(FLEncoder* encoder);
        void FLEncoder_SetSharedKeys(FLEncoder* encoder, FLSharedKeys* shared);
        void FLEncoder_SetExtraInfo(FLEncoder* e, void* info);
        void* FLEncoder_GetExtraInfo(FLEncoder* e);
        void FLEncoder_MakeDelta(FLEncoder* e, byte[] @base, bool reuseStrings);
        void FLEncoder_Reset(FLEncoder* encoder);
        bool FLEncoder_WriteNull(FLEncoder* encoder);
        bool FLEncoder_WriteBool(FLEncoder* encoder, bool b);
        bool FLEncoder_WriteInt(FLEncoder* encoder, long l);
        bool FLEncoder_WriteUInt(FLEncoder* encoder, ulong u);
        bool FLEncoder_WriteFloat(FLEncoder* encoder, float f);
        bool FLEncoder_WriteDouble(FLEncoder* encoder, double d);
        bool FLEncoder_WriteString(FLEncoder* encoder, string str);
        bool FLEncoder_WriteData(FLEncoder* encoder, byte[] slice);
        bool FLEncoder_WriteRaw(FLEncoder* encoder, byte[] slice);
        bool FLEncoder_BeginArray(FLEncoder* encoder, ulong reserveCount);
        bool FLEncoder_EndArray(FLEncoder* encoder);
        bool FLEncoder_BeginDict(FLEncoder* encoder, ulong reserveCount);
        bool FLEncoder_WriteKey(FLEncoder* encoder, string str);
        bool FLEncoder_EndDict(FLEncoder* encoder);
        bool FLEncoder_WriteValue(FLEncoder* encoder, FLValue* value);
        bool FLEncoder_WriteValueWithSharedKeys(FLEncoder* encoder, FLValue* value, FLSharedKeys* shared);
        bool FLEncoder_ConvertJSON(FLEncoder* e, byte[] json);
        UIntPtr FLEncoder_BytesWritten(FLEncoder* e);
        byte[] FLEncoder_Finish(FLEncoder* encoder, FLError* outError);
        FLError FLEncoder_GetError(FLEncoder* e);
        string FLEncoder_GetErrorMessage(FLEncoder* encoder);
    }

    internal unsafe interface ILiteCoreRaw
    {
        C4SliceResult c4error_getMessage(C4Error error);
        C4Error c4error_make(C4ErrorDomain domain, int code, C4Slice message);
        bool c4log_writeToBinaryFile(C4LogLevel level, C4Slice path, C4Error* error);
        byte* c4log_getDomainName(C4LogDomain* x);
        void c4slog(C4LogDomain* domain, C4LogLevel level, C4Slice msg);
        C4SliceResult c4_getBuildInfo();
        C4SliceResult c4_getVersion();
        bool c4blob_keyFromString(C4Slice str, C4BlobKey* x);
        C4SliceResult c4blob_keyToString(C4BlobKey key);
        C4BlobStore* c4blob_openStore(C4Slice dirPath, C4DatabaseFlags flags, C4EncryptionKey* encryptionKey, C4Error* outError);
        C4SliceResult c4blob_getContents(C4BlobStore* store, C4BlobKey key, C4Error* outError);
        C4SliceResult c4blob_getFilePath(C4BlobStore* store, C4BlobKey key, C4Error* outError);
        C4BlobKey c4blob_computeKey(C4Slice contents);
        bool c4blob_create(C4BlobStore* store, C4Slice contents, C4BlobKey* expectedKey, C4BlobKey* outKey, C4Error* error);
        UIntPtr c4stream_read(C4ReadStream* stream, byte[] buffer, UIntPtr maxBytesToRead, C4Error* outError);
        bool c4stream_write(C4WriteStream* stream, byte[] bytes, UIntPtr length, C4Error* outError);
        C4Database* c4db_open(C4Slice path, C4DatabaseConfig* config, C4Error* outError);
        bool c4db_copy(C4Slice sourcePath, C4Slice destinationPath, C4DatabaseConfig* config, C4Error* error);
        bool c4db_deleteAtPath(C4Slice dbPath, C4Error* outError);
        C4SliceResult c4db_getPath(C4Database* db);
        C4RawDocument* c4raw_get(C4Database* database, C4Slice storeName, C4Slice docID, C4Error* outError);
        bool c4raw_put(C4Database* database, C4Slice storeName, C4Slice key, C4Slice meta, C4Slice body, C4Error* outError);
        bool c4doc_isOldMetaProperty(C4Slice prop);
        C4SliceResult c4doc_encodeStrippingOldMetaProperties(FLDict* doc);
        C4SliceResult c4doc_bodyAsJSON(C4Document* doc, bool canonical, C4Error* outError);
        C4SliceResult c4db_encodeJSON(C4Database* db, C4Slice jsonData, C4Error* outError);
        FLDictKey c4db_initFLDictKey(C4Database* db, C4Slice @string);
        C4Document* c4doc_get(C4Database* database, C4Slice docID, bool mustExist, C4Error* outError);
        bool c4doc_selectRevision(C4Document* doc, C4Slice revID, bool withBody, C4Error* outError);
        C4SliceResult c4doc_detachRevisionBody(C4Document* doc);
        bool c4doc_selectFirstPossibleAncestorOf(C4Document* doc, C4Slice revID);
        bool c4doc_selectNextPossibleAncestorOf(C4Document* doc, C4Slice revID);
        bool c4doc_selectCommonAncestorRevision(C4Document* doc, C4Slice rev1ID, C4Slice rev2ID);
        uint c4db_getRemoteDBID(C4Database* db, C4Slice remoteAddress, bool canCreate, C4Error* outError);
        C4SliceResult c4db_getRemoteDBAddress(C4Database* db, uint remoteID);
        C4SliceResult c4doc_getRemoteAncestor(C4Document* doc, uint remoteDatabase);
        uint c4rev_getGeneration(C4Slice revID);
        int c4doc_purgeRevision(C4Document* doc, C4Slice revID, C4Error* outError);
        bool c4doc_resolveConflict(C4Document* doc, C4Slice winningRevID, C4Slice losingRevID, C4Slice mergedBody, C4RevisionFlags mergedFlags, C4Error* error);
        bool c4db_purgeDoc(C4Database* database, C4Slice docID, C4Error* outError);
        bool c4doc_setExpiration(C4Database* db, C4Slice docId, ulong timestamp, C4Error* outError);
        ulong c4doc_getExpiration(C4Database* db, C4Slice docId);
        C4Document* c4doc_put(C4Database* database, C4DocPutRequest* request, UIntPtr* outCommonAncestorIndex, C4Error* outError);
        C4Document* c4doc_create(C4Database* db, C4Slice docID, C4Slice body, C4RevisionFlags revisionFlags, C4Error* error);
        C4Document* c4doc_update(C4Document* doc, C4Slice revisionBody, C4RevisionFlags revisionFlags, C4Error* error);
        C4SliceResult c4exp_getDocID(C4ExpiryEnumerator* e);
        C4DocumentObserver* c4docobs_create(C4Database* database, C4Slice docID, C4DocumentObserverCallback callback, void* context);
        C4Query* c4query_new(C4Database* database, C4Slice expression, C4Error* error);
        C4SliceResult c4query_explain(C4Query* query);
        C4QueryEnumerator* c4query_run(C4Query* query, C4QueryOptions* options, C4Slice encodedParameters, C4Error* outError);
        C4SliceResult c4query_fullTextMatched(C4Query* query, C4FullTextMatch* term, C4Error* outError);
        bool c4db_createIndex(C4Database* database, C4Slice name, C4Slice expressionsJSON, C4IndexType indexType, C4IndexOptions* indexOptions, C4Error* outError);
        bool c4db_deleteIndex(C4Database* database, C4Slice name, C4Error* outError);
        C4SliceResult c4db_getIndexes(C4Database* database, C4Error* outError);
        bool c4repl_isValidDatabaseName(C4Slice dbName);
        bool c4address_fromURL(C4Slice url, C4Address* address, C4Slice* dbName);
        C4SliceResult c4address_toURL(C4Address address);
        bool c4db_setCookie(C4Database* db, C4Slice setCookieHeader, C4Slice fromHost, C4Slice fromPath, C4Error* outError);
        C4SliceResult c4db_getCookies(C4Database* db, C4Address request, C4Error* error);
        void c4socket_closeRequested(C4Socket* socket, int status, C4Slice message);
        void c4socket_completedWrite(C4Socket* socket, UIntPtr byteCount);
        void c4socket_received(C4Socket* socket, C4Slice data);
        FLValue* FLValue_FromData(FLSlice data);
        FLValue* FLValue_FromTrustedData(FLSlice data);
        FLSliceResult FLData_ConvertJSON(FLSlice json, FLError* outError);
        FLSliceResult FLData_Dump(FLSlice data);
        FLSlice FLValue_AsString(FLValue* value);
        FLSlice FLValue_AsData(FLValue* value);
        FLSliceResult FLValue_ToString(FLValue* value);
        FLSliceResult FLValue_ToJSON(FLValue* value);
        FLSliceResult FLValue_ToJSON5(FLValue* v);
        FLSliceResult FLValue_ToJSONX(FLValue* v, FLSharedKeys* sk, bool json5, bool canonicalForm);
        FLSliceResult FLJSON5_ToJSON(FLSlice json5, FLError* error);
        FLValue* FLDict_Get(FLDict* dict, FLSlice keyString);
        FLValue* FLDict_GetSharedKey(FLDict* d, FLSlice keyString, FLSharedKeys* sk);
        FLSlice FLSharedKey_GetKeyString(FLSharedKeys* sk, int keyCode, FLError* outError);
        FLValue* FLDict_GetUnsorted(FLDict* dict, FLSlice keyString);
        FLSlice FLDictIterator_GetKeyString(FLDictIterator* i);
        FLDictKey FLDictKey_Init(FLSlice @string, bool cachePointers);
        FLDictKey FLDictKey_InitWithSharedKeys(FLSlice @string, FLSharedKeys* sharedKeys);
        FLSlice FLDictKey_GetString(FLDictKey* key);
        UIntPtr FLDict_GetWithKeys(FLDict* dict, FLDictKey[] keys, FLValue[] values, UIntPtr count);
        FLKeyPath* FLKeyPath_New(FLSlice specifier, FLSharedKeys* shared, FLError* error);
        FLValue* FLKeyPath_EvalOnce(FLSlice specifier, FLSharedKeys* shared, FLValue* root, FLError* error);
        FLEncoder* FLEncoder_NewWithOptions(FLEncoderFormat format, UIntPtr reserveSize, bool uniqueStrings, bool sortKeys);
        void FLEncoder_MakeDelta(FLEncoder* e, FLSlice @base, bool reuseStrings);
        bool FLEncoder_WriteString(FLEncoder* encoder, FLSlice str);
        bool FLEncoder_WriteData(FLEncoder* encoder, FLSlice slice);
        bool FLEncoder_WriteRaw(FLEncoder* encoder, FLSlice slice);
        bool FLEncoder_BeginArray(FLEncoder* encoder, UIntPtr reserveCount);
        bool FLEncoder_BeginDict(FLEncoder* encoder, UIntPtr reserveCount);
        bool FLEncoder_WriteKey(FLEncoder* encoder, FLSlice str);
        bool FLEncoder_ConvertJSON(FLEncoder* e, FLSlice json);
        FLSliceResult FLEncoder_Finish(FLEncoder* encoder, FLError* outError);
    }
}
