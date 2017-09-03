using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using Microsoft.Azure.Mobile.Server;
using Microsoft.Azure.Mobile.Server.Tables;
using VigBE.DataObjects;
using System.Data.Entity.Validation;
using VigBE.DataObjects.DBO_Models;
using VigBE.DataObjects.DTO_Mappers;

namespace VigBE.Models
{
    public class MobileServiceContext : DbContext
    {
        // You can add custom code to this file. Changes will not be overwritten.
        // 
        // If you want Entity Framework to alter your database
        // automatically whenever you change your model schema, please use data migrations.
        // For more information refer to the documentation:
        // http://msdn.microsoft.com/en-us/data/jj591621.aspx
        //
        // To enable Entity Framework migrations in the cloud, please ensure that the 
        // service name, set by the 'MS_MobileServiceName' AppSettings in the local 
        // Web.config, is the same as the service name when hosted in Azure.

        private const string connectionStringName = "Name=MS_TableConnectionString";

        public MobileServiceContext() : base(connectionStringName)
        {
        }

        public DbSet<AllEventsModel> AllEventsModels { get; set; }
        public DbSet<Attending> Attendings { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<MessageModel> MessageModels { get; set; }
        public DbSet<MediaFileModel> MediaFileModels { get; set; }
        public DbSet<EventChat> EventChats { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<TodoItem> TodoItems { get; set; }
        public DbSet<UserModel> UsersModels { get; set; }
        public DbSet<Venue> Venues { get; set; }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {

            /*
            modelBuilder.Entity<MessageModel>()
                .Ignore(m => m.AllEventsModel);

            modelBuilder.Entity<AllEventsModel>()
               .HasMany(e => e.ChatModels)
               .WithRequired()
               .Map(k => k.MapKey("AllEventsModel_Id"));
            // .HasForeignKey(k => k.);
            //    .HasForeignKey(d => new { d.DepartmentID, d.DepartmentName });
            */
            /*
            modelBuilder.Entity<AllEventsModel>()
            .HasMany(m => m.ChatModels)
            .WithMany()
            .Map(k =>
            {
                k.ToTable("EventChat");
                k.MapLeftKey("EventId");
                k.MapRightKey("MessageId");
            });
            */

            /*
            modelBuilder.Entity<UserModel>()
              .HasMany(m => m.AllEventsModels)
              .WithMany(u => u.UserModels)
              .Map(k =>
              {
                  k.ToTable("Attending");
                  k.MapLeftKey("UserId");
                  k.MapRightKey("EventId");
              });
              */

            modelBuilder.Conventions.Add(
                new AttributeToColumnAnnotationConvention<TableColumnAttribute, string>(
                    "ServiceTableColumn", (property, attributes) => attributes.Single().ColumnType.ToString()));
        }

        public override int SaveChanges()
        {
            try
            {
                return base.SaveChanges();
            }
            catch (DbEntityValidationException ex)
            {
                // Retrieve the error messages as a list of strings.
                var errorMessages = ex.EntityValidationErrors
                        .SelectMany(x => x.ValidationErrors)
                        .Select(x => x.ErrorMessage);

                // Join the list to a single string.
                var fullErrorMessage = string.Join("; ", errorMessages);

                // Combine the original exception message with the new one.
                var exceptionMessage = string.Concat(ex.Message, " The validation errors are: ", fullErrorMessage);

                // Throw a new DbEntityValidationException with the improved exception message.
                throw new DbEntityValidationException(exceptionMessage, ex.EntityValidationErrors);
            }
        }
        
    }
}
