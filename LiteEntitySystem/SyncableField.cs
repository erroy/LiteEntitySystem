using System;
using System.Runtime.CompilerServices;
using LiteEntitySystem.Internal;

namespace LiteEntitySystem
{
    public abstract class SyncableField
    {
        internal InternalEntity ParentEntity;
        internal ExecuteFlags Flags;
        internal ushort RPCOffset;

        protected internal virtual void OnSyncRequested()
        {
            
        }

        protected internal virtual void RegisterRPC(ref SyncableRPCRegistrator r)
        {

        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected void ExecuteRPC(in RemoteCall rpc)
        {
            ((Action<SyncableField>)rpc.CachedAction)?.Invoke(this);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected void ExecuteRPC<T>(in RemoteCall<T> rpc, T value) where T : unmanaged
        {
            ((Action<SyncableField, T>)rpc.CachedAction)?.Invoke(this, value);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected void ExecuteRPC<T>(in RemoteCallSpan<T> rpc, ReadOnlySpan<T> value) where T : unmanaged
        {
            ((SpanAction<SyncableField, T>)rpc.CachedAction)?.Invoke(this, value);
        }
    }
}