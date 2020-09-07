using System;
using Elementary;

namespace OregoFramework.Unit
{
    public abstract class Resource : Element, IResource
    {
        #region Const

        protected const string RESOURCE_WORD = "Resource";

        protected const int FIRST_INDEX = 0;

        #endregion

        public virtual string path
        {
            get { return this.GetDefaultResourceName(); }
        }

        private string defaultName;

        protected virtual string GetDefaultResourceName()
        {
            if (this.defaultName != null)
            {
                return this.defaultName;
            }

            var name = this.GetType().Name;
            if (name.StartsWith(RESOURCE_WORD))
            {
                this.defaultName = name.Substring(FIRST_INDEX + RESOURCE_WORD.Length, name.Length);
                return this.defaultName;
            }

            if (name.EndsWith(RESOURCE_WORD))
            {
                this.defaultName = name.Substring(FIRST_INDEX, name.Length - RESOURCE_WORD.Length);
                return this.defaultName;
            }

            throw new Exception("The prefab name " + name + " doesn't start/end with "
                                + RESOURCE_WORD + "!" +
                                " Please, rename prefab or override property \'path\'!");
        }
    }
}