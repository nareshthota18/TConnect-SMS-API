namespace RSMS.Api.Extentions
{
    public static class HttpContextExtensions
    {
        //public static Guid GetRSHostelId(this HttpContext context)
        //    => (Guid)context.Items["RSHostelId"];

        public static Guid GetRSHostelId(this HttpContext context)
        {
            // 1. Get the claim value using the User property on HttpContext
            var rsHostelIdClaim = context.User.FindFirst("RSHostelId");

            // 2. Check if the claim exists and has a value that can be parsed as a Guid
            if (rsHostelIdClaim != null &&
                Guid.TryParse(rsHostelIdClaim.Value, out Guid rsHostelId))
            {
                // 3. Return the parsed Guid
                return rsHostelId;
            }

            // 4. Return an empty Guid if the claim is missing, null, or invalid
            return Guid.Empty;
        }
    }
}
