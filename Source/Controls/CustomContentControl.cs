using Source.MVVM.ViewModel;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Source.Controls
{

    public class CustomContentControl : ContentControl
    {
        private Shape? m_paintArea;
        private ContentPresenter? m_mainContent;

        static CustomContentControl()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(CustomContentControl), new FrameworkPropertyMetadata(typeof(CustomContentControl)));
        }

        public override void OnApplyTemplate()
        {
            m_paintArea = Template.FindName("PART_PaintArea", this) as Shape;
            m_mainContent = Template.FindName("PART_MainContent", this) as ContentPresenter;
            base.OnApplyTemplate();
        }

        private Brush CreateBrushVisual(Visual v)
        {
            if (v == null)
                throw new ArgumentNullException("v");

            var target = new RenderTargetBitmap((int)ActualWidth, (int)ActualHeight, 96, 96, PixelFormats.Pbgra32);
            target.Render(v);

            var brush = new ImageBrush(target);
            brush.Freeze();
            return brush;
        }

        private void BeginAnimateContentReplacement()
        {
            var newContentTransform = new TranslateTransform();
            var oldContentTransform = new TranslateTransform();

            m_paintArea.RenderTransform = oldContentTransform;
            m_mainContent.RenderTransform = newContentTransform;

            m_paintArea.Visibility = Visibility.Visible;

            newContentTransform.BeginAnimation(TranslateTransform.XProperty, CreateAnimation(ActualWidth, 0));
            oldContentTransform.BeginAnimation(TranslateTransform.XProperty, CreateAnimation(0, -ActualWidth, (s, e) => m_paintArea.Visibility = Visibility.Hidden));
        }

        private AnimationTimeline CreateAnimation(double from, double to, EventHandler whenDone = null)
        {
            IEasingFunction ease = new CubicEase
            { EasingMode = EasingMode.EaseInOut };
            var duration = new Duration(TimeSpan.FromSeconds(0.35));
            var anim = new DoubleAnimation(from, to, duration)
            { EasingFunction = ease };
            if (whenDone != null)
                anim.Completed += whenDone;
            anim.Freeze();
            return anim;
        }

        protected override void OnContentChanged(object oldContent, object newContent)
        {
            ((ViewModelBase)oldContent)?.UnloadCommand.Execute(null);
            ((ViewModelBase)newContent)?.LoadCommand.Execute(null);
            if (m_paintArea != null && m_mainContent != null)
            {
                m_paintArea.Fill = CreateBrushVisual(m_mainContent);
                BeginAnimateContentReplacement();
            }
            base.OnContentChanged(oldContent, newContent);
        }
    }
}
