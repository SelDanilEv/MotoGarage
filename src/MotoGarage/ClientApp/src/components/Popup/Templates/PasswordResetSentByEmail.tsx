import React from 'react';
import Popup from '../Popup';
import ShowResetMessageSend from './Models/ShowResetMessageSend';

const PasswordResetSentByEmail = (props: any) => {
    return (
        <Popup
            setShowPopup={props.setShowPopup}
            data={
                <ShowResetMessageSend
                    setShowPopup={props.setShowPopup}
                    item={props.item}
                />}
        />
    );
}

export default PasswordResetSentByEmail;