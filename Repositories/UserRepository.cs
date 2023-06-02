using System.Data;
using MyFirstProject.Models;

namespace MyFirstProject.Repositories
{
    public class UserRepository
    {
        #region ADONETInstance
        /*
        DbHelper dbHelper = new DbHelper();
        List<SqlParameter> parameterList;
        DataTable dt;
        string sql;
        */
        #endregion

        public IEnumerable<User> GetUser(string username, string password)
        {

            AdminContext context = new AdminContext();
            IEnumerable<User> users = context.Users;

            return users.Where(x => x.Username == username && x.Password == password).ToList();

            #region withADONET
            /*
            User user = new User();
            sql = "select Id, Name, Surname, Email, Username, Password, IsAdmin from [Users] where username=@USERNAME and password=@PASSWORD";
            parameterList = new List<SqlParameter>()
            {
                new SqlParameter("@USERNAME", username),
                new SqlParameter("@PASSWORD", password)
            };

            dt = dbHelper.ExecuteReader(sql, parameterList);

            try
            {
                DataRow dr = dt.Rows[0];
                user = new User
                {
                    Id = Convert.ToInt32(dr.ItemArray[0].ToString()),
                    Name = dr.ItemArray[1].ToString(),
                    Surname = dr.ItemArray[2].ToString(),
                    Email = dr.ItemArray[3].ToString(),
                    Username = dr.ItemArray[4].ToString(),
                    Password = dr.ItemArray[5].ToString(),
                    IsAdmin = Convert.ToBoolean(dr.ItemArray[6])
                };
                return user;
            }
            catch (Exception ex) { return null; }
            */
            #endregion
        }

        public void CreateUser(User user)
        {
            AdminContext context = new AdminContext();
            context.Users.Add(user);
            context.SaveChanges();

            #region withADONET
            /*
            sql = "insert into [Users] (Name, Surname, Email, Username, Password, IsAdmin) values (@NAME, @SURNAME, @EMAIL, @USERNAME, @PASSWORD, @ISADMIN)";
            parameterList = new List<SqlParameter>()
            {
                new SqlParameter("@NAME", user.Name),
                new SqlParameter("@SURNAME", user.Surname),
                new SqlParameter("@EMAIL", user.Email),
                new SqlParameter("@USERNAME", user.Username),
                new SqlParameter("@PASSWORD", user.Password),
                new SqlParameter("@ISADMIN", user.IsAdmin)
            };

            return dbHelper.ExecuteNonQuery(sql, parameterList);
            */
            #endregion
        }
    }
}
