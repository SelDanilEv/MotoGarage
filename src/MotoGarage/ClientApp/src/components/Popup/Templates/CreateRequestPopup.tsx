import React from 'react';
import CreateServiceRequest from './Models/CreateServiceRequest';
import Popup from '../Popup';

const CreateRequestPopup = (props: any) => {
    return (
        <Popup
            setShowPopup={props.setShowPopup}
            data={
                <CreateServiceRequest
                    setShowPopup={props.setShowPopup}
                />}
        />
    );
}

export default CreateRequestPopup;