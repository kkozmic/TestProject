<%@ Application Language="C#" %>
<%@ Import Namespace="System.Reflection" %>
<%@ Import Namespace="Castle.ActiveRecord.Framework" %>
<%@ Import Namespace="Castle.ActiveRecord" %>
<%@ Import Namespace="Wawel.DomainModel" %>
<script runat="server">
void Application_Start(object sender, EventArgs e)
{
    IConfigurationSource source = ConfigurationManager.GetSection("activerecord") as IConfigurationSource;
    ActiveRecordStarter.Initialize(new Assembly[]{
    // list assemblies containing your ActiveRecord models here, for example like
    // typeof(MyApp.DomainModel.MyEntity).Assembly,
    typeof(Benchmark).Assembly
    }, source);

    // uncomment the following line if you wish ActiveRecord to create your database schema
    
    ActiveRecordStarter.CreateSchema();

    HttpContext.Current.Response.Redirect(@"~\Home\Default");
}
</script>