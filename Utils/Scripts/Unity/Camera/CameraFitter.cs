using UnityEngine;

namespace OregoFramework.Util
{
    public sealed class CameraFitter : MonoBehaviour
    {
        [SerializeField]
        private float defaultSize = 5.5f;

        [SerializeField]
        private float defaultRelation = 1920 / 1080;

        [SerializeField]
        private float sizeCoef = 10;

        [SerializeField]
        private bool isOrthographic;

        [SerializeField]
        private new Camera camera;

        private void Awake()
        {
            this.FitSize();
        }

        private void FitSize()
        {
            var relation = (float) Screen.width / Screen.height;
            var sizeOffset = (this.defaultRelation - relation) * this.sizeCoef;
            var targetSize = this.defaultSize + sizeOffset;
            
            if (this.isOrthographic)
            {
                this.camera.orthographicSize = targetSize;
            }
            else
            {
                this.camera.fieldOfView = targetSize;
            }
        }
    }
}