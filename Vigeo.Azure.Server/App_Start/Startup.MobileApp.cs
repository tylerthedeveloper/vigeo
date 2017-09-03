using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Web.Http;
using Microsoft.Azure.Mobile.Server;
using Microsoft.Azure.Mobile.Server.Authentication;
using Microsoft.Azure.Mobile.Server.Config;
using VigBE.DataObjects;
using VigBE.Models;
using Owin;
//using VigBE.Migrations;
using System.Data.Entity.Migrations;
using VigBE.DataObjects.DBO_Models;
using VigBE.DataObjects.DTO_Mappers;

namespace VigBE
{
    public partial class Startup
    {
        public static void ConfigureMobileApp(IAppBuilder app)
        {
            var config = new HttpConfiguration();
            var mobileConfig = new MobileAppConfiguration();

            config.IncludeErrorDetailPolicy = IncludeErrorDetailPolicy.Always;

            mobileConfig
                .AddTablesWithEntityFramework()
                .ApplyTo(config);


            //Database.SetInitializer(new MobileServiceInitializer());
            //Database.SetInitializer(new MigrateDatabaseToLatestVersion<MobileServiceContext, Migrations.Configuration>());

            var migrator = new DbMigrator(new Migrations.Configuration());
            migrator.Update();


            app.UseWebApi(config);
        }

    }
}

