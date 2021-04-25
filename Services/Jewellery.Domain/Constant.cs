namespace Jewellery.Domain
{
    public static class Constant
    {
        public const string TokenHeader = "X-Token";
        public const string Template = @"select u.Id, u.FirstName, u.LastName, u.UserId, p.Type from 
                                            [dbo].[userinfo] info join [dbo].[user] u
                                            on info.UserId = u.Id
                                            join [dbo].[Privilege] p 
                                            on info.PrivilegeId = p.Id
                                            where info.UserId= {0}";
        public const string GlobalErrorMessage = "Please contact admin!";
        public const string ValidationAssembly = "Jewellery.Application";
        public const string ConnectionString = "DbConnectionString";
        public const string AllowedHeaders = "AllowedHeaders";
        public const string ClientIp = "ClientIp";
    }
}
