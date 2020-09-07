using Elementary;
using OregoFramework.Unit;

namespace OregoFramework.App
{
    public abstract class EncryptablePrefsDao<T> : PrefsDao<T>
    {
        protected virtual string hash { get; private set; }

        protected sealed override void OnCreate(Element _, IElementContext context)
        {
            this.hash = this.GetType().Name;
            this.OnCreate(this);
        }

        protected virtual void OnCreate(EncryptablePrefsDao<T> _)
        {
        }

        protected override string SerializeEntity(T entity)
        {
            var serializedEntity = base.SerializeEntity(entity);
            var encryptedEntity = Security.Encrypt(serializedEntity, this.hash);
            return encryptedEntity;
        }

        protected override T DeserializeEntity(string stringData)
        {
            var decryptedEntity = Security.Decrypt(stringData, this.hash);
            var deserializedEntity = base.DeserializeEntity(decryptedEntity);
            return deserializedEntity;
        }
    }
}