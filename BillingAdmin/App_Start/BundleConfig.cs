using System.Web;
using System.Web.Optimization;

namespace BillingAdmin
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        { 
            #region Standart bundles            
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Используйте версию Modernizr для разработчиков, чтобы учиться работать. Когда вы будете готовы перейти к работе,
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap/bootstrap.css"
                      , "~/Content/site.css"));
            #endregion

            #region SITE            
            //Styles Library Site
            bundles.Add(new StyleBundle("~/StylesLibrarySite").Include(
                "~/Content/bootstrap/bootstrap.css",
                "~/Content/bootstrap-datetimepicker.css",
                "~/Content/owlcarousel/owl.carousel.min.css",
                "~/Content/owlcarousel/owl.theme.default.css",
                "~/Content/jBox/jBox.css"));

            #region Styles Shared Site            
            bundles.Add(new StyleBundle("~/StylesSharedSite").Include(
                "~/Content/Shared/styles.css",
                "~/Content/Shared/media.css"
                ));
            #endregion

            #region Scripts Library Site            
            bundles.Add(new ScriptBundle("~/ScriptsLibrarySite").Include(
                "~/Scripts/jquery/jquery-3.2.1.min.js",
                "~/Scripts/jquery.unobtrusive-ajax.min.js",
                "~/Scripts/jquery/jquery-migrate-3.0.0.min.js",
                "~/Scripts/moment.min.js",
                "~/Scripts/moment-with-locales.min.js",
                "~/Scripts/bootstrap/bootstrap.min.js",
                "~/Scripts/bootstrap-datetimepicker.min.js",
                "~/Scripts/owlcarousel/owl.carousel.min.js",
                "~/Scripts/maskedinput/jquery.maskedinput.js",
                "~/Scripts/jBox/jBox.js"));
            #endregion

            //Scripts Shared Site
            bundles.Add(new ScriptBundle("~/ScriptsSharedSite").Include(
                "~/Scripts/SharedSite/script.js",
                "~/Scripts/SharedSite/form.js",
                "~/Scripts/SharedSite/audit.js"
                ));
            #endregion

            #region LK
            //Styles Library
            bundles.Add(new StyleBundle("~/StylesLibrary").Include(
                "~/Content/bootstrap/bootstrap.css",
                "~/Content/bootstrap-datetimepicker.css",
                "~/Content/owlcarousel/owl.carousel.min.css",
                "~/Content/owlcarousel/owl.theme.default.css",
                "~/Content/jBox/jBox.css",
                "~/Content/bootstrap-select.css",
                "~/Scripts/jquery/jquery-3.2.1.min.js",
                "~/Scripts/jquery/jquery-migrate-3.0.0.min.js",
                "~/Scripts/moment/moment.min.js",
                "~/Scripts/moment/moment-with-locales.min.js",
                "~/Scripts/bootstrap/bootstrap.min.js",
                "~/Scripts/bootstrap/bootstrap-datetimepicker.min.js",
                "~/Scripts/maskedinput/jquery.maskedinput.js"
                ));
            //Styles DataTable
            bundles.Add(new StyleBundle("~/StylesDataTable").Include(
                "~/Content/datatable.buttons/datatables.css"
                ));
            //Styles Shared
            bundles.Add(new StyleBundle("~/StylesShared").Include(
                "~/Content/Shared/styles.css",
                "~/Content/Shared/media.css",
                "~/Content/Shared/lk-styles.css",
                   "~/Content/Shared/styles-admin.css",
                   "~/Content/Shared/header.css",
                   "~/Content/Shared/buttons.css"
                ));

            //Scripts Library
            bundles.Add(new ScriptBundle("~/ScriptsLibrary").Include(
                "~/Scripts/jquery/jquery-3.2.1.min.js",
                "~/Scripts/jquery.unobtrusive-ajax.min.js",
                "~/Scripts/jquery/jquery-migrate-3.0.0.min.js",
                "~/Scripts/moment.min.js",
                "~/Scripts/moment-with-locales.min.js",
                "~/Scripts/bootstrap/bootstrap.min.js",
                "~/Scripts/bootstrap-datetimepicker.min.js",
                "~/Scripts/owlcarousel/owl.carousel.min.js",
                "~/Scripts/maskedinput/jquery.maskedinput.js",
                "~/Scripts/jBox/jBox.js",
                "~/Scripts/bootstrap-select.js"
                ));
            //Scripts DataTable
            bundles.Add(new ScriptBundle("~/ScriptsDataTable").Include(
                "~/Scripts/jquery.dataTables.min.js",
                "~/Scripts/datatable.buttons/datatables.min.js",
                "~/Scripts/datatable.rowgroup/dataTables.rowGroup.min.js"
                ));
            //Scripts Validate
            bundles.Add(new ScriptBundle("~/ScriptsValidate").Include(
                "~/Scripts/jquery/jquery.validate.js"
                ));
            //Scripts Shared
            bundles.Add(new ScriptBundle("~/ScriptsShared").Include(
                "~/Scripts/Shared/script.js"
                /*"~/Scripts/js/_jquery-ui.js",
                "~/Scripts/js/_lodash.min.js",
                "~/Scripts/js/_gridstack.min.js",
                "~/Scripts/js/_gridstack.jQueryUI.min.js",
                "~/Scripts/js/_projects.js",
                "~/Scripts/js/_script.js"*/ /*отключено для релиза статистика*/
                ));
            #endregion
        }
    }
}
