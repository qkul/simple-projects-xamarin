using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Runtime;
using Android.Widget;
using System;

namespace CitiesList
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : ListActivity
    {
       
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            string[] countries = Resources.GetStringArray(Resource.Array.countries_arr);
            ListAdapter = new ArrayAdapter<string>(this, Resource.Layout.list_item, countries);
        }
    }
}