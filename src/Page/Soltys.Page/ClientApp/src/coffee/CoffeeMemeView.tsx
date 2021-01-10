import React from 'react';

import { ViewContainer } from '../basic/Basic'
import memsImage from './mems/smacznej_kawusi.jpg';


const CoffeeMemeView = () => {
    return (
        <ViewContainer>
            <img src={memsImage} alt="coffee meme" />
        </ViewContainer>
    )
};

export default CoffeeMemeView