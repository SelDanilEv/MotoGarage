import { useContext } from "react";
import { LoadingContext } from "./LoadingStore";

const wrapAPICall = async (
  action: () => Promise<void>,
  setLoadingState: (state: any) => void
) => {
  try {
    setLoadingState({ type: "START_LOADING", payload: null });
    await action();
  } catch (error) {
    console.error(error);
  } finally {
    setLoadingState({ type: "STOP_LOADING", payload: null });
  }
};

export default wrapAPICall;
