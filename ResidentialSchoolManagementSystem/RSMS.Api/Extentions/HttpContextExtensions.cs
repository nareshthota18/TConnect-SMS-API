namespace RSMS.Api.Extentions
{
    public static class HttpContextExtensions
    {
        public static Guid GetRSHostelId(this HttpContext context)
            => (Guid)context.Items["RSHostelId"];
    }

}
