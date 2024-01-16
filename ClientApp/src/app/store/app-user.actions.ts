import { createAction, props } from '@ngrx/store';
import { IUser } from '../user/user.model';

export const set = createAction('[AppUser] Set',props<{UserProfile: IUser}>());
export const get = createAction('[AppUser] Get');
export const reset = createAction('[AppUser] Reset');