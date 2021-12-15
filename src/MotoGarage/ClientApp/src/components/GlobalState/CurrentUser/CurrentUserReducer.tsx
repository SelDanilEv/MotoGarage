import { CurrentUserStorageName } from "./CurrentUserStore";

const CurrentUserReducer = (state: any, action: any) => {
  switch (action.type) {
    case "SET_USER":
      localStorage.setItem(CurrentUserStorageName,
        JSON.stringify(action.payload));
        console.log("EndSET")
      return {
        ...state,
        CurrentUser: action.payload,
      };
    case "CLEAN_USER":
      localStorage.removeItem(CurrentUserStorageName);
      return {
        ...state,
        CurrentUser: undefined,
      };
    default:
      return state;
  }
};

export default CurrentUserReducer;
