using System.Configuration;
using System.IO;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using TMD.WebBase;
using TMD.WebBase.UnityConfiguration;
using Microsoft.Practices.Unity;
using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Logging;
using System.Web.Http;
using UnityDependencyResolver = TMD.WebBase.UnityConfiguration.UnityDependencyResolver;
using System;
using System.Web;

namespace IdentitySample
{
    public class MvcApplication : System.Web.HttpApplication
    {
        #region Private
        private static IUnityContainer container;
        /// <summary>
        /// Configure Logger
        /// </summary>
        private void ConfigureLogger()
        {
            DatabaseFactory.SetDatabaseProviderFactory(new DatabaseProviderFactory());
            IConfigurationSource configurationSource = ConfigurationSourceFactory.Create();
            LogWriterFactory logWriterFactory = new LogWriterFactory(configurationSource);
            Logger.SetLogWriter(logWriterFactory.Create());
        }
        /// <summary>
        /// Create the unity container
        /// </summary>
        private static IUnityContainer CreateUnityContainer()
        {
            container = UnityWebActivator.Container;
            RegisterTypes();

            return container;
        }
        /// <summary>
        /// Register types with the IoC
        /// </summary>
        private static void RegisterTypes()
        {
            TypeRegistrations.RegisterTypes(container);
            TMD.Implementation.TypeRegistrations.RegisterType(container);

        }
        /// <summary>
        /// Register unity 
        /// </summary>
        private static void RegisterIoC()
        {
            if (container == null)
            {
                container = CreateUnityContainer();
            }
        }
        #endregion
        protected void Application_Start()
        {
            RegisterIoC();
            AreaRegistration.RegisterAllAreas();
            ConfigureLogger();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters, container);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            // Set MVC resolver
            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
            // Set Web Api resolver
            GlobalConfiguration.Configuration.DependencyResolver = new UnityDependencyResolver(container);
        }
        private void Session_Start(object sender, EventArgs e)
        {
            try
            {
                #region Visitor counter

                //int count = 0;
                //count = GetAndUpdateCounterValue();
                //Session["VisitorCounter"] = count;

                #endregion
            }
            catch (Exception ex)
            {
                
            }
            }

        private int GetAndUpdateCounterValue()
        {
            StreamReader stmReader;
            StreamWriter stmWriter;
            FileStream fileStream;
            string fileContents;
            int counter = 0;
            string filePath = Server.MapPath(ConfigurationManager.AppSettings["CounterFilePath"]);

            #region Read File
                if (File.Exists(filePath))
                {
                    stmReader = File.OpenText(filePath);
                    fileContents = stmReader.ReadLine();
                    if (fileContents != null)
                    {
                        counter = Convert.ToInt32(fileContents);
                    }
                    stmReader.Close();
                }
                else
                {
                    counter = 0;
                }
            #endregion

            counter++;
            #region Write File
                fileStream =new FileStream(filePath,FileMode.OpenOrCreate,FileAccess.Write);
                stmWriter=new StreamWriter(fileStream);
                stmWriter.WriteLine(Convert.ToString(counter));
                stmWriter.Close();
            #endregion

            return counter;
        }
        protected void Application_BeginRequest()
        {
            //On each request, it will disable the creation of Cache for that request/page.
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.Cache.SetExpires(DateTime.UtcNow.AddHours(-1));
            Response.Cache.SetNoStore();
        }
    }
}