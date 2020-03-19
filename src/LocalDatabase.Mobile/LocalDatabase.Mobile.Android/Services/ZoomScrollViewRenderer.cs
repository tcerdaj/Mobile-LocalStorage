using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Gestures;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Views.Animations;
using Android.Widget;
using LocalDatabase.Mobile.CustomControls;
using LocalDatabase.Mobile.Droid.Services;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(CustomScrollView), typeof(ZoomScrollViewRenderer))]
namespace LocalDatabase.Mobile.Droid.Services
{
    public class ZoomScrollViewRenderer : ScrollViewRenderer, ScaleGestureDetector.IOnScaleGestureListener
    {
        private float mScale = 1f;
        private ScaleGestureDetector mScaleDetector;

        protected override void OnElementChanged(VisualElementChangedEventArgs e)
        {
            base.OnElementChanged(e);

            mScaleDetector = new ScaleGestureDetector(Context, this);
        }

        public override bool DispatchTouchEvent(MotionEvent e)
        {
            base.DispatchTouchEvent(e);
            mScaleDetector.OnTouchEvent(e);

            return mScaleDetector.OnTouchEvent(e);
        }

        public bool OnScale(ScaleGestureDetector detector)
        {
            float scale = 1 - detector.ScaleFactor;

            float prevScale = mScale;
            mScale += scale;

            if (mScale < 0.2f) // Minimum scale condition:
                mScale = 0.2f;

            if (mScale > 1f) // Maximum scale condition:
                mScale = 1f;
            
            System.Diagnostics.Debug.WriteLine($"detector.FocusX: {detector.FocusX}, detector.FocusY: {detector.FocusY}");
            System.Diagnostics.Debug.WriteLine($"mScale: {mScale}");

            ScaleAnimation scaleAnimation = new ScaleAnimation(1f / prevScale, 1f / mScale, 1f / prevScale, 1f / mScale, detector.FocusX + 10, detector.FocusY);
            scaleAnimation.Duration = 0;
            scaleAnimation.FillAfter = true;
            StartAnimation(scaleAnimation);
            return true;
        }

        public bool OnScaleBegin(ScaleGestureDetector detector)
        {
            return true;
        }

        public void OnScaleEnd(ScaleGestureDetector detector)
        {
            
        }
    }

    public class OnGestureListener : GestureDetector.SimpleOnGestureListener
    {
        public override bool OnDown(MotionEvent e)
        {
            return true;
        }

        public override bool OnDoubleTap(MotionEvent e)
        {
            return true;
        }
    }
}