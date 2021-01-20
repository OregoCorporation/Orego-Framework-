using OregoFramework.Util;

namespace OregoFramework.App
{
    public abstract class EncryptablePrefsDao<T> : PrefsDao<T>
    {
        protected virtual string Hash { get; private set; }

        protected sealed override void OnCreate()
        {
            base.OnCreate();
            this.Hash = this.GetType().Name;
        }
        
        protected override string SerializeEntity(T entity)
        {
            var serializedEntity = base.SerializeEntity(entity);
            var encryptedEntity = SecurityUtils.Encrypt(serializedEntity, this.Hash);
            return encryptedEntity;
        }

        protected override T DeserializeEntity(string stringData)
        {
            var decryptedEntity = SecurityUtils.Decrypt(stringData, this.Hash);
            var deserializedEntity = base.DeserializeEntity(decryptedEntity);
            return deserializedEntity;
        }
    }
}
