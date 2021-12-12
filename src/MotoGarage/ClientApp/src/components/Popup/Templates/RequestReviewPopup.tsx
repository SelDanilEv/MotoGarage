import React from 'react';
import CreateRequestReview from '../../Requests/CreateRequestReview';
import Popup from '../Popup';

const RequestReviewPopup = (props: any) => {
    return (
        <Popup
            setShowPopup={props.setShowPopup}
            data={
                <CreateRequestReview
                    setShowPopup={props.setShowPopup}
                    item={props.item}
                />}
        />
    );
}

export default RequestReviewPopup;