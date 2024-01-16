export interface IUser {
  id:number;
  fullName: string;
  userName: string;
  email: string;
  token: string;
  role: number;
}

export interface IUserCredentials {
  userName: string;
  password: string;
}
