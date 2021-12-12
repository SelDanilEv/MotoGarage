const CurrentUserReducer = (state: any, action: any) => {
  switch (action.type) {
    case "SET_USER":
      return {
        ...state,
        CurrentUser: action.payload,
      };
    case "CLEAN_USER":
      return {
        ...state,
        CurrentUser: undefined,
      };
    default:
      return state;
  }
};

export default CurrentUserReducer;
