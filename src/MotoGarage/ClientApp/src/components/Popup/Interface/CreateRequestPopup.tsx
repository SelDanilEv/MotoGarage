import { ReactNode } from "react";

export default interface PopupProps {
    data: ReactNode;
    setShowPopup: React.Dispatch<React.SetStateAction<boolean>>;
  }