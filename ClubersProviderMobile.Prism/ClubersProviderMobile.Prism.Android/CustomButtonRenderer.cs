using Android.Content;
using ClubersProviderMobile.Prism.CustomRender;
using ClubersProviderMobile.Prism.Droid;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(CustomButton), typeof(CustomButtonRenderer))]

namespace ClubersProviderMobile.Prism.Droid
{
    public class CustomButtonRenderer : ButtonRenderer
    {
        public CustomButtonRenderer(Context context) : base(context)
        {
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Xamarin.Forms.Button> e)
        {
            base.OnElementChanged(e);
            var button = this.Control;
            button.SetAllCaps(false);
        }
    }

}