import React from 'react';
import AccountInfoPage from './Models/AccountInfoPage';
import Popup from '../Popup';

const UpdateUserInfoPopup = (props: any) => {
    return (
        <Popup
            setShowPopup={props.setShowPopup}
            data={
                <AccountInfoPage
                    setShowPopup={props.setShowPopup}
                />}
        />
    );
}

export default UpdateUserInfoPopup;