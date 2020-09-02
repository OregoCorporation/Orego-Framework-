using System;
using System.Collections;
using System.Collections.Generic;
using Elementary;
using OregoFramework.Util;
using UnityEngine;
using Object = UnityEngine.Object;

namespace OregoFramework.Unit
{
    /// <summary>
    ///     <para>Loads resources instead invoking of method "Resources.Load".</para>
    ///     <para>Keeps lightweight resource instances instead assets.</para>
    ///     <para>Uses for avoid hardcoding string path.</para>
    /// </summary>
    public abstract class Resourcer : ElementLayer<IResource>, IRootElement
    {
        #region Const

        public static readonly string EXCEPTION_MESSAGE =
            $"{nameof(Resourcer)} is absent in context! " +
            $"Please create a class derived from {nameof(Resourcer)} " +
            $"and add attribute {nameof(Using)} over the class!";

        #endregion

        public static bool isCreated
        {
            get { return instance != null; }
        }

        private static Resourcer instance;

        public override void OnCreate(IElementContext context)
        {
            base.OnCreate(context);
            instance = this;
        }

        #region Get

        public static IEnumerable<T> GetResources<T>() where T : IResource
        {
            return GetInstance().GetElements<T>();
        }

        public static IEnumerable<IResource> GetResources()
        {
            return GetInstance().GetElements<IResource>();
        }

        public static T GetResource<T>() where T : IResource
        {
            return GetInstance().GetElement<T>();
        }

        #endregion

        #region Load

        public static T Load<T, R>() 
            where T : Object 
            where R : IResource
        {
            var resource = instance.GetElement<R>();
            var resourcePath = resource.path;
            return Resources.Load<T>(resourcePath);
        }

        #endregion

        #region AsyncLoad

        public static IEnumerator LoadAsync<T>(IResource resource, Reference<T> result)
            where T : Object
        {
            yield return LoadAsync(resource.path, result);
        }

        public static IEnumerator LoadAsync<T>(string path, Reference<T> result) where T : Object
        {
            var request = Resources.LoadAsync<T>(path);
            yield return request;
            var asset = request.asset;
            result.value = (T) asset;
        }

        #endregion

        private static Resourcer GetInstance()
        {
            if (!isCreated)
            {
                throw new Exception(EXCEPTION_MESSAGE);
            }

            return instance;
        }
    }
}