const LoadingReducer = (state: any, action: any) => {
  switch (action.type) {
    case "START_LOADING":
      state.CallsCounter += 1;
      return {
        ...state,
        Loading: true,
      };
    case "STOP_LOADING":
      state.CallsCounter -= 1;
      return {
        ...state,
        Loading: state.CallsCounter > 1,
      };
    default:
      return state;
  }
};

export default LoadingReducer;
