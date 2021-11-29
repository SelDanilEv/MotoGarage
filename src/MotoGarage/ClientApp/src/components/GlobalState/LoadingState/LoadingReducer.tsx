const LoadingReducer = (state: any, action: any) => {
  switch (action.type) {
    case "START_LOADING":
      return {
        ...state,
        Loading: true,
      };
    case "STOP_LOADING":
      return {
        ...state,
        Loading: false,
      };
    default:
      return state;
  }
};

export default LoadingReducer;
