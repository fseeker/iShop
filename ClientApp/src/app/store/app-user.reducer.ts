import { createReducer, on } from '@ngrx/store';
import { set, get, reset } from './app-user.actions';
import { IUser } from '../user/user.model';

export const initialState = {} as IUser;

export const appUserReducer = createReducer(
  initialState,
  on(set, (state, {UserProfile}) => UserProfile),
  //on(get, (state) => state),
  on(reset, () => initialState)
);

