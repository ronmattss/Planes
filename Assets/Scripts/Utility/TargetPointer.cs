using UnityEngine;
using UnityEngine.UI;

namespace Utility
{
    public class TargetPointer : MonoBehaviour
    {
       [SerializeField] private Camera uiCamera;
        private Vector3 targetPosition;
        private RectTransform pointerRectTransform;
        [SerializeField] private Image image;
        public GameObject missile; 
        [SerializeField]private RectTransform canvas;
        
        void Awake()
        {
            canvas = GameplayManager.Instance.canvas;
            uiCamera = GameplayManager.Instance.uiCamera;
            this.transform.parent = canvas;
            targetPosition = missile.transform.position; // test
            pointerRectTransform = this.gameObject.GetComponent <RectTransform>();
        }

        void Update()
        {
            float bordersize = 100f;
            Vector3 toPosition = targetPosition;
            Vector3 fromPosition = Camera.main.transform.position;
            fromPosition.z = 0f;
            Vector3 dir = (toPosition - fromPosition).normalized;
            float angle = ProjectileComputation.GetAngleFromVectorFloat(dir);
            pointerRectTransform.localEulerAngles = new Vector3(0,0,angle);


            Vector3 targetPositionScreenPoint = Camera.main.WorldToScreenPoint(targetPosition);

            bool isOffScreen = targetPositionScreenPoint.x <= bordersize || targetPositionScreenPoint.x >= Screen.width  - bordersize||
                               targetPositionScreenPoint.y <= bordersize || targetPositionScreenPoint.y >= Screen.height - bordersize;

            if (isOffScreen)
            {
                image.enabled = true;
                Vector3 cappedTargetScreenPosition = targetPositionScreenPoint;
                if (cappedTargetScreenPosition.x <= bordersize) cappedTargetScreenPosition.x = bordersize;
                if (cappedTargetScreenPosition.x >= Screen.width - bordersize) cappedTargetScreenPosition.x = Screen.width - bordersize;
                if (cappedTargetScreenPosition.y <= 0) cappedTargetScreenPosition.y = bordersize;
                if (cappedTargetScreenPosition.y >= Screen.height - bordersize) cappedTargetScreenPosition.y = Screen.height - bordersize;

                Vector3 pointerWorldPosition = uiCamera.ScreenToWorldPoint((cappedTargetScreenPosition));
                pointerRectTransform.position = pointerWorldPosition;
                pointerRectTransform.localPosition = new Vector3(pointerRectTransform.localPosition.x,pointerRectTransform.localPosition.y,0f);
            }
            else
            {
                image.enabled = false;
            }

            if (missile == null)
            {
                Destroy(this.gameObject);
            }
        }
    }
}