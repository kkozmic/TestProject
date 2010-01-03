<%@ Application Language="C#" %>
<%@ Import Namespace="System.Reflection" %>
<%@ Import Namespace="Castle.ActiveRecord.Framework" %>
<script runat="server">
void Application_Start(object sender, EventArgs e)
{
    IConfigurationSource source = ConfigurationManager.GetSection("activerecord") as IConfigurationSource;
    if(source== null)
    {
        HttpContext.Current.Response.Write("Can't find 'activerecord' section in web.config file");
        HttpContext.Current.Response.End();
        return;
    }
    Castle.ActiveRecord.ActiveRecordStarter.Initialize(new Assembly[]{
    // list assemblies containing your ActiveRecord models here, for example like
    // typeof(MyApp.DomainModel.MyEntity).Assembly,
    }, source);
    
    HttpContext.Current.Response.Redirect(@"~\Home\Default");
}
</script>