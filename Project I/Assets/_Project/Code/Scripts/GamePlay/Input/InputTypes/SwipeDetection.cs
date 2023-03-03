using System;
using UnityEngine;
using Wonderland.Utility;

namespace Wonderland.GamePlay.Input
{
    [RequireComponent(typeof(InputManager))]
    public class SwipeDetection : IControls
    {
        #region Setting
        
        //
        private float minimumDistance = .1f;
        //
        private float maximumTime = .5f;
        //
        private float directionThreshold = .9f;
        
        #endregion

        #region Fields

        //
        private Vector2 startPosition;
        //
        private float startTime;
        //
        private Vector2 endPosition;
        //
        private float endTime;

        #endregion

        #region Swipe Events

        //
        public static event Action LeftSwipe;
        //
        public static event Action RightSwipe;
        //
        public static event Action UpSwipe;
        //
        public static event Action DownSwipe;

        #endregion
        
        #region Methods
        
        private void SwipeStart(Vector2 screenPosition, GameObject swiped, float time)
        {
            startPosition = screenPosition;
            startTime = time;
        }
        
        private void SwipeEnd(Vector2 screenPosition, GameObject swiped, float time)
        {
            endPosition = screenPosition;
            endTime = time;
            DetectSwipe();
        }
        
        private void DetectSwipe()
        {
            if (Vector3.Distance(startPosition, endPosition) >= minimumDistance && (endTime - startTime) <= maximumTime)
            {
                Logging.InputControls.Log("Swipe Detected");
                Vector3 direction = endPosition - startPosition;
                Vector2 direction2D = new Vector2(direction.x, direction.y).normalized;
                SwipeDirection(direction2D);
            }
        }
        
        private void SwipeDirection(Vector2 direction)
        {
            if (Vector2.Dot(Vector2.up, direction) > directionThreshold)
            {
                UpSwipe?.Invoke();
                Logging.InputControls.Log("SwipeUp");
            } 
            else if (Vector2.Dot(Vector2.down, direction) > directionThreshold)
            {
                DownSwipe?.Invoke();
                Logging.InputControls.Log("SwipeDown");
            } 
            else if (Vector2.Dot(Vector2.left, direction) > directionThreshold)
            {
                LeftSwipe?.Invoke();
                Logging.InputControls.Log("SwipeLeft");
            } 
            else if (Vector2.Dot(Vector2.right, direction) > directionThreshold)
            {
                RightSwipe.Invoke();
                Logging.InputControls.Log("SwipeRight");
            } 
        }

        #endregion
        

        public override void OnEnable()
        {
            base.OnEnable();
            InputManager.Instance.OnStartPrimaryTouchEvent += SwipeStart;
            InputManager.Instance.OnEndPrimaryTouchEvent += SwipeEnd;
        }

        public override void OnDisable()
        {
            base.OnDisable();
            InputManager.Instance.OnStartPrimaryTouchEvent -= SwipeStart;
            InputManager.Instance.OnEndPrimaryTouchEvent -= SwipeEnd;
        }
    }
}