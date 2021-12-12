import React from 'react';
import CreateServiceRequest from '../../Requests/CreateServiceRequest';
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