using iShopServerSide.Model;
using Microsoft.Data.SqlClient;

namespace iShopServerSide.DAL
{
    public class UserRepository
    {
        private SqlConnection sqlConn;

        public UserRepository(SqlConnection sqlConn)
        {
            this.sqlConn = sqlConn;
        }

        //public bool Login2(UserCred credentials)
        //{
        //    try
        //    {
        //        bool successfulLogin = false;

        //        string sql = @"
        //            IF EXISTS( SELECT 1 FROM UserCredentialsTbl WHERE Username = @username AND UserPassword = @password)
        //                BEGIN
        //                	PRINT 'Login Success';
        //                	select 1;
        //                END
        //                ELSE
        //                BEGIN
        //                	PRINT 'Login failed';
        //                	select 0;
        //                END
        //            ";

        //        using (var cmd = new SqlCommand(sql, sqlConn))
        //        {
        //            cmd.Parameters.AddWithValue("@username", credentials.UserName);
        //            cmd.Parameters.AddWithValue("@password", credentials.Password);

        //            sqlConn.Open();

        //            successfulLogin = Convert.ToInt32(cmd.ExecuteScalar()) == 1 ? true : false;

        //            return successfulLogin;
        //        }
        //    }
        //    catch(Exception ex)
        //    {
        //        throw new Exception(ex.Message);
        //    }
        //    finally
        //    {
        //        sqlConn.Close();
        //    }
        //}

        public (bool, UserProfile) Login(UserCred credentials)
        {
            try
            {
                bool successfulLogin = false;

                string sql = @"
                    Select * from UserDetailsTbl UD
		            JOIN UserCredentialsTbl UC on UD.UserId = UC.Id
		            where UC.UserName = @username AND UC.UserPassword = @password
                    ";

                using (var cmd = new SqlCommand(sql, sqlConn))
                {
                    cmd.Parameters.AddWithValue("@username", credentials.UserName);
                    cmd.Parameters.AddWithValue("@password", credentials.Password);

                    sqlConn.Open();

                    //successfulLogin = Convert.ToInt32(cmd.ExecuteScalar()) == 1 ? true : false;

                    using (var reader = cmd.ExecuteReader()) { 
                        if(!reader.HasRows)
                        {
                            return (false, null);
                        }

                        reader.Read();

                        var response = new UserProfile()
                        {
                            Id = reader.GetInt32(0),
                            UserName = reader.GetString(7),
                            FullName = reader.GetString(1) + " " + reader.GetString(2) + " " + reader.GetString(3),
                            Email = reader.GetString(4),
                            Role = 1
                        };
                        return (true, response);
                    }

                    
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                sqlConn.Close();
            }
        }

        public bool Signup(UserCred credentials)
        {
            try
            {
                bool successfulSignUp = false;

                string sql = @"
                    IF EXISTS( SELECT 1 FROM UserCredentialsTbl WHERE Username = @username)
                        BEGIN
                        	PRINT 'user already exists';
                        	select 0;
                        END
                        ELSE
                        BEGIN
                        	INSERT INTO [dbo].[UserCredentialsTbl]
                                   ([UserName]
                                   ,[UserPassword])
                            VALUES
                                   (@username, @password);
                        
                        	PRINT 'user created';
                        	select 1;
                        END
                    ";

                using (var cmd = new SqlCommand(sql, sqlConn))
                {
                    cmd.Parameters.AddWithValue("@username", credentials.UserName);
                    cmd.Parameters.AddWithValue("@password", credentials.Password);

                    sqlConn.Open();

                    successfulSignUp = Convert.ToInt32(cmd.ExecuteScalar()) == 1 ? true : false;
                }

                    return successfulSignUp;
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                sqlConn.Close();
            }
        }
    }
}
