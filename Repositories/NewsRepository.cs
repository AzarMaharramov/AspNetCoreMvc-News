using Microsoft.EntityFrameworkCore;
using MyFirstProject.Models;
using System.Data;
using System.Data.SqlClient;

namespace MyFirstProject.Repositories
{
    public class NewsRepository
    {
        #region ADO.NETInstance
        /*
        DbHelper dbHelper = new DbHelper();
        List<SqlParameter> parameterList;
        DataTable dt;
        string sql;
        */
        #endregion

        public IQueryable<News> GetAllNews()
        {
            AdminContext context = new AdminContext();
            IQueryable<News> news = context.News;

            return news;

            #region withADONET
            /*
            List<News> list = new List<News>();
            sql = "select Id, Title, Date, Image, Content, Status from [News] order by Id desc";
            parameterList = new List<SqlParameter>();
            dt = dbHelper.ExecuteReader(sql, parameterList);

            foreach (DataRow dr in dt.Rows)
            {
                list.Add(new News
                {
                    Id = Convert.ToInt32(dr.ItemArray[0].ToString()),
                    Title = dr.ItemArray[1].ToString(),
                    Date = Convert.ToDateTime(dr.ItemArray[2].ToString()),
                    Image = dr.ItemArray[3].ToString(),
                    Content = dr.ItemArray[4].ToString(),
                    Status = Convert.ToBoolean(dr.ItemArray[5].ToString())
                });
            }

            return list;
            */
            #endregion
        }

        public async Task<News?> GetNewsAsync(int id)
        {
            AdminContext context = new AdminContext();
            News? news = await context.News.FirstOrDefaultAsync(x => x.Id == id);

            return news;

            #region withADONET
            /*
            News news = new News();
            sql = "select Id, Title, Date, Image, Content, Status from [News] where id=@ID";
            parameterList = new List<SqlParameter>() { new SqlParameter("@ID", id) };
            dt = dbHelper.ExecuteReader(sql, parameterList);

            try
            {
                DataRow dr = dt.Rows[0];
                news = new News
                {
                    Id = Convert.ToInt32(dr.ItemArray[0].ToString()),
                    Title = dr.ItemArray[1].ToString(),
                    Date = Convert.ToDateTime(dr.ItemArray[2].ToString()),
                    Image = dr.ItemArray[3].ToString(),
                    Content = dr.ItemArray[4].ToString(),
                    Status = Convert.ToBoolean(dr.ItemArray[5].ToString())
                };
                return news;
            }
            catch (Exception ex) { return null; }
            */
            #endregion
        }

        public void AddNews(News news)
        {
            AdminContext context = new AdminContext();
            context.News.Add(news);
            context.SaveChanges();

            #region withADONET
            /*
            sql = "insert into [News] (Title, Date, Image, Content, Status) values (@TITLE, @DATE, @IMAGE, @CONTENT, @STATUS)";

            parameterList = (new List<SqlParameter>()
            {
                new SqlParameter("@TITLE", news.Title),
                new SqlParameter("@DATE", news.Date),
                new SqlParameter("@IMAGE", news.Image),
                new SqlParameter("@CONTENT", news.Content),
                new SqlParameter("@STATUS", news.Status),
            });

            return dbHelper.ExecuteNonQuery(sql, parameterList);
            */
            #endregion
        }
    }
}
