export interface IUser {
  firstName: string;
  lastName: string;
  email: string;
  token: string;
  role: string;
}

export interface IUserCredentials {
  userName: string;
  password: string;
}
