import { Button } from '@mui/material';
import React from 'react';
import PopupProps from './Interface/CreateRequestPopup';
import './style.css';



const Popup = (props: PopupProps) => {
    return (
        <div className='popup'>
            <div className='popup_open'>
                <Button
                    className='close_popup_button'
                    onClick={() => props.setShowPopup(false)}>
                    X
                </Button>
                {props.data}
            </div>
        </div>
    );
}

export default Popup;