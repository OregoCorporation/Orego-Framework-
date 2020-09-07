using Elementary;
using OregoFramework.Unit;

namespace OregoFramework.App
{
    /// <summary>
    ///     <para>Player prefs data access object with security mechanism.</para>
    /// </summary>
    public abstract class EncryptablePrefsDao : PrefsDao
    {
        protected virtual string hash { get; private set; }

        protected sealed override void OnCreate(Element _, IElementContext context)
        {
            this.hash = this.GetType().Name;
            this.OnCreate(this);
        }

        protected virtual void OnCreate(EncryptablePrefsDao _)
        {
        }

        protected sealed override string SerializeEntity<T>(T entity)
        {
            var serializedEntity = base.SerializeEntity(entity);
            var encryptedEntity = Security.Encrypt(serializedEntity, this.hash);
            return encryptedEntity;
        }

        protected sealed override T DeserializeEntity<T>(string stringData)
        {
            var decryptedEntity = Security.Decrypt(stringData, this.hash);
            var deserializedEntity = base.DeserializeEntity<T>(decryptedEntity);
            return deserializedEntity;
        }
    }
}