using HiQ.NetStandard.Util.Data;
using System;
using System.Collections.Generic;
using System.Data;

using VHS.Entity;

namespace VHSBackend.Core.Repository
{
    public class SqlUserRepository : ADbRepositoryBase, IUserRepository
    {
        //#region Public Methods
        
        //public IList<User> GetUserList()
        //{
        //    IList<User> userList = new List<User>();
            
        //    var dr = DbAccess.ExecuteReader("dbo.sUsers_List", CommandType.StoredProcedure);

        //    while (dr.Read())
        //    {
        //        //userList.Add(new User()
        //        //{
        //        //    Id = dr.GetGuid(0),
        //        //    FirstName = dr.GetString(1),
        //        //    LastName = dr.GetString(2),
        //        //    UserLevel = (Enums.UserLevel)dr.GetInt32(3)
        //        //});
        //        userList.Add(ParseUserDataRow<User>(ref dr));
        //    }

        //    DbAccess.DisposeReader(ref dr);
            
        //    return userList;

        //}

        //public IList<User> SearchUser(string searchString)
        //{
            
        //    IList<User> userList = new List<User>();

        //    if (!string.IsNullOrEmpty(searchString))
        //    {
        //        searchString = searchString.Replace("*", "%");

        //        if (!searchString.StartsWith("%"))
        //        {
        //            searchString = $"%{searchString}";
        //        }

        //        if (!searchString.EndsWith("%"))
        //        {
        //            searchString = $"{searchString}%";
        //        }

        //        if (searchString.Length >= 4)
        //        {

        //            var parameters = new SqlParameters();
        //            parameters.AddSearchString(searchString);

        //            var dr = DbAccess.ExecuteReader("dbo.sUser_Search", ref parameters, CommandType.StoredProcedure);
        //            while (dr.Read())
        //            {
        //                userList.Add(ParseUserDataRow<User>(ref dr));
        //            }

        //            DbAccess.DisposeReader(ref dr);
        //        }
        //    }

        //    return userList;
        //}

        //public Guid CreateUser(UserFull user)
        //{
        //    if (ValidateUserDto(user))
        //    {

        //        var conn = new SqlConnection(ServiceProvider.Current.Configuration.ConnectionStrings
        //            .TrashVacDbConnectionString);
        //        conn.Open();

        //        var pwdHash = Common.GenerateMd5Hash(user.Pwd);

        //        var cmd = new SqlCommand("dbo.sUser_Create", conn) { CommandType = CommandType.StoredProcedure };
        //        cmd.Parameters.Add(new SqlParameter("@UserName", SqlDbType.VarChar, 255) { Value = user.UserName });
        //        cmd.Parameters.Add(new SqlParameter("@PwdHash", SqlDbType.NVarChar, 255) { Value = pwdHash });
        //        cmd.Parameters.Add(new SqlParameter("@FirstName", SqlDbType.NVarChar, 50) { Value = user.FirstName });
        //        cmd.Parameters.Add(new SqlParameter("@LastName", SqlDbType.NVarChar, 100) { Value = user.LastName });
        //        cmd.Parameters.Add(new SqlParameter("@UserLevel", SqlDbType.Int) { Value = (int)user.UserLevel });
        //        cmd.Parameters.Add(new SqlParameter("@UserId", SqlDbType.UniqueIdentifier)
        //            { Value = null, Direction = ParameterDirection.Output });

        //        cmd.ExecuteNonQuery();

        //        var userId = new Guid(cmd.Parameters[5].Value.ToString());

        //        conn.Close();

        //        return userId;
        //    }
        //    return Guid.Empty;
            
        //}

        //public Enums.PersistUserTagRelationStatus PersistUserTagRelation(Guid userId, UserRfIdRelation dto)
        //{
        //    var user = GetUserById(userId);
        //    if (user != null)
        //    {
        //        if (dto != null && dto.UserId.Equals(userId))
        //        {
        //            var parameters = new SqlParameters();
        //            parameters.AddUserId(userId);
        //            parameters.AddRfId(dto.RfId);
        //            parameters.AddDeleted(dto.Deleted);
        //            DbAccess.ExecuteNonQuery("[dbo].[sUserRdIdRealation_Persist]", ref parameters,
        //                CommandType.StoredProcedure);
        //            return Enums.PersistUserTagRelationStatus.Success;

        //        }

        //        return Enums.PersistUserTagRelationStatus.InvalidDto;
        //    }

        //    return Enums.PersistUserTagRelationStatus.UserNotFound;
        //}

        //public UserWithTags GetUserTags(Guid userId)
        //{
        //    UserWithTags user = null;

        //    var parameters = new SqlParameters();
        //    parameters.AddUserId(userId);

        //    var index = 0;
        //    var dr = DbAccess.ExecuteReader("dbo.sUser_GetRfIdTags", ref parameters, CommandType.StoredProcedure);
            
        //    while (dr.Read())
        //    {
        //        if (index == 0)
        //        {
        //            user = ParseUserDataRow<UserWithTags>(ref dr);
        //        }

        //        if (user != null)
        //        {
        //            if (!dr.IsDBNull(4) && !dr.IsDBNull(5))
        //            {
        //                user.Tags.Add(new RfIdTag() { RfId = dr.GetString(4), Description = dr.GetString(5) });
        //            }
        //        }

        //        index++;
        //    }

        //    DbAccess.DisposeReader(ref dr);
        //    return user;
        //}

        //public bool TryLogin(string userName, string password, out UserAuthenticated user)
        //{
        //    var result = false;

        //    user = null;
        //    var conn = new SqlConnection(ServiceProvider.Current.Configuration.ConnectionStrings
        //        .TrashVacDbConnectionString);
        //    conn.Open();
        //    var cmd = new SqlCommand("dbo.sUser_GetByUserName", conn) { CommandType = CommandType.StoredProcedure };
        //    cmd.Parameters.Add(new SqlParameter("@UserName", SqlDbType.VarChar, 255) { Value = userName });
        //    cmd.Parameters.Add(new SqlParameter("@Result", SqlDbType.Bit) { Direction = ParameterDirection.Output });
        //    cmd.Parameters.Add(new SqlParameter("@UserId", SqlDbType.UniqueIdentifier)
        //        { Direction = ParameterDirection.Output });
        //    cmd.Parameters.Add(new SqlParameter("@PwdHash", SqlDbType.NVarChar, 255) { Direction = ParameterDirection.Output });
        //    cmd.Parameters.Add(new SqlParameter("@FirstName", SqlDbType.NVarChar, 50) { Direction = ParameterDirection.Output });
        //    cmd.Parameters.Add(new SqlParameter("@LastName", SqlDbType.NVarChar, 100) { Direction = ParameterDirection.Output });
        //    cmd.Parameters.Add(new SqlParameter("@UserLevel", SqlDbType.Int)  { Direction = ParameterDirection.Output });

        //    cmd.ExecuteNonQuery();

        //    if (Convert.ToBoolean(cmd.Parameters[1].Value))
        //    {
        //        var dbPwdHash = cmd.Parameters[3].Value.ToString();
        //        var pwdHash = Common.GenerateMd5Hash(password);

        //        if (dbPwdHash.Equals(pwdHash, StringComparison.Ordinal))
        //        {
        //            result = true;
        //            user = new UserAuthenticated()
        //            {
        //                Id = new Guid(cmd.Parameters[2].Value.ToString()),
        //                UserName = userName,
        //                FirstName = cmd.Parameters[4].Value.ToString(),
        //                LastName = cmd.Parameters[5].Value.ToString(),
        //                UserLevel = (Enums.UserLevel)Convert.ToInt32(cmd.Parameters[6].Value), 
        //                AccessToken = GenerateAccessToken()
        //            };
        //            UpdateAccessToken(user.Id, user.AccessToken);

        //        }
        //    }

        //    conn.Close();
            
        //    return result;
        //}

        //public bool ValidateToken(string token, out User user)
        //{
        //    var result = false;
        //    user = null;

        //    var conn = new SqlConnection(ServiceProvider.Current.Configuration.ConnectionStrings
        //        .TrashVacDbConnectionString);
        //    conn.Open();
        //    var cmd = new SqlCommand("dbo.sUser_ValidateToken", conn) { CommandType = CommandType.StoredProcedure };
        //    cmd.Parameters.Add(new SqlParameter("@Token", SqlDbType.NVarChar, 255) { Value = token });
        //    cmd.Parameters.Add(new SqlParameter("@TTL", SqlDbType.Int)
        //        { Value = ServiceProvider.Current.Configuration.SiteSettings.AccessTokenTtl });
        //    cmd.Parameters.Add(new SqlParameter("@Result", SqlDbType.Bit) { Direction = ParameterDirection.Output });
        //    cmd.Parameters.Add(new SqlParameter("@UserId", SqlDbType.UniqueIdentifier)
        //        { Direction = ParameterDirection.Output });


        //    cmd.ExecuteNonQuery();

        //    result = Convert.ToBoolean(cmd.Parameters[2].Value);

        //    if (result)
        //    {
        //        user = GetUserById(new Guid(cmd.Parameters[3].Value.ToString()));
        //    }

        //    conn.Close();

        //    return result;
        //}

        //public User GetUserById(Guid userId)
        //{
            

        //    var parameters = new SqlParameters();
        //    parameters.AddUniqueIdentifier("@UserId", userId);
        //    parameters.AddBoolean("@Result", false, ParameterDirection.Output);
        //    parameters.AddVarChar("@UserName", 255, string.Empty, ParameterDirection.Output);
        //    parameters.AddNVarChar("@FirstName", 50, string.Empty, ParameterDirection.Output);
        //    parameters.AddNVarChar("@LastName", 100, string.Empty, ParameterDirection.Output);
        //    parameters.AddInt("@UserLevel", 0, ParameterDirection.Output);

        //    DbAccess.ExecuteNonQuery("dbo.sUser_GetById", ref parameters, CommandType.StoredProcedure);

        //    if (parameters.GetBool("@Result"))
        //    {
        //        return new User()
        //        {
        //            Id = userId,
        //            UserName = parameters.GetString("@UserName"),
        //            FirstName = parameters.GetString("@FirstName"),
        //            LastName = parameters.GetString("@LastName"),
        //            UserLevel = (Enums.UserLevel)parameters.GetInt("@UserLevel")
        //        };
        //    }

        //    return null;
        //}
        //#endregion
        
        //#region Private Methods

        //private T ParseUserDataRow<T>(ref SqlDataReader dr)
        //{

        //    var user = Activator.CreateInstance<T>();

        //    if (user is User)
        //    {
        //        (user as User).Id = dr.GetGuid(0);
        //        (user as User).FirstName = dr.GetString(1);
        //        (user as User).LastName = dr.GetString(2);
        //        (user as User).UserLevel = (Enums.UserLevel)dr.GetInt32(3);
        //    }
           
        //    return user;
        //}
        //private string GenerateAccessToken()
        //{
        //    const string VALID_CHARS = "0123456789abcdefghijklmnopqrstuvwx";

        //    var token = string.Empty;
        //    var done = false;
        //    var random = new Random();

        //    while (!done)
        //    {
        //        token = string.Empty;
        //        for (var i = 0; i < 50; i++)
        //        {
        //            var index = random.Next(0, VALID_CHARS.Length - 1);

        //            token = $"{token}{VALID_CHARS.Substring(index, 1)}";
                    
        //        }
        //        done = IsTokenUnique(token);
        //    }


        //    return token;


        //}

        //private bool IsTokenUnique(string token)
        //{
        //    var conn = new SqlConnection(ServiceProvider.Current.Configuration.ConnectionStrings
        //        .TrashVacDbConnectionString);
        //    conn.Open();
        //    var cmd = new SqlCommand("dbo.sUser_IsTokenUnique", conn) { CommandType = CommandType.StoredProcedure };
        //    cmd.Parameters.Add(new SqlParameter("@Token", SqlDbType.NVarChar, 255) { Value = token });
        //    cmd.Parameters.Add(new SqlParameter("@Result", SqlDbType.Bit) { Direction = ParameterDirection.Output });

        //    cmd.ExecuteNonQuery();
        //    var result = Convert.ToBoolean(cmd.Parameters[1].Value);
        //    conn.Close();
        //    return result;
        //}

        //private void UpdateAccessToken(Guid userId, string token)
        //{
        //    var conn = new SqlConnection(ServiceProvider.Current.Configuration.ConnectionStrings
        //        .TrashVacDbConnectionString);
        //    conn.Open();

        //    var cmd = new SqlCommand("[dbo].[sUser_PersistToken]", conn) { CommandType = CommandType.StoredProcedure };

        //    cmd.Parameters.Add(new SqlParameter("@UserId", SqlDbType.UniqueIdentifier) { Value = userId });
        //    cmd.Parameters.Add(new SqlParameter("@Token", SqlDbType.NVarChar, 255) { Value = token });
        //    cmd.Parameters.Add(new SqlParameter("@TTL", SqlDbType.Int)
        //        { Value = ServiceProvider.Current.Configuration.SiteSettings.AccessTokenTtl });

        //    cmd.ExecuteNonQuery();

        //    conn.Close();
        //}

        //private bool ValidateUserDto(UserFull user)
        //{
        //    if (user == null)
        //    {
        //        return false;
        //    }

        //    var result = !string.IsNullOrEmpty(user.FirstName) && !string.IsNullOrEmpty(user.LastName) &&
        //                 !string.IsNullOrEmpty(user.UserName);
        //    return result;
        //}

        //#endregion
    }
}
