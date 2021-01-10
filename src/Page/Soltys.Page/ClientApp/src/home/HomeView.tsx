import React from 'react';
import { ViewContainer, BoxContainer, Box, ExternalOnly } from '../basic/Basic'
import { Link } from "react-router-dom";
import CoffeeImg from './coffee.jpg'
import CVImg from './cv.jpg'
import PizzaImg from './pizza.jpg'
import GithubImg from './github.png';
import BlogImg from './blog.jpg';
import './HomeView.css';

type LinkInfographicProps = {
    linkTo: string,
    content: string,
    image: string,
    imageAlt: string,
}

type InfographicProps = {
    content: string,
    image: string,
    imageAlt: string,
}

const Infographic: React.FunctionComponent<InfographicProps> = (props) => {
    return (
        <div className="infographic">
            <img src={props.image} alt={props.imageAlt} />
            <div className="infographic-note">
                {props.content}
            </div>
        </div>
    )
}

const LocalInfographic: React.FunctionComponent<LinkInfographicProps> = (props) => {
    return (
        <Link to={props.linkTo}>
            <Infographic content={props.content} image={props.image} imageAlt={props.imageAlt} />
        </Link>
    );
}

const ExternalInfographic: React.FunctionComponent<LinkInfographicProps> = (props) => {
    return (
        <a href={props.linkTo}>
            <Infographic content={props.content} image={props.image} imageAlt={props.imageAlt} />
        </a>
    );
}

const HomeView = () => {
    return (
        <ViewContainer>
            <div className="infographic-section">
                <BoxContainer>
                    <Box>
                        <LocalInfographic
                            linkTo="/coffee"
                            content="Kawa"
                            image={CoffeeImg}
                            imageAlt="kawa" />
                    </Box>
                    <Box>
                        <LocalInfographic
                            linkTo="/pizza"
                            content="Pizza"
                            image={PizzaImg}
                            imageAlt="pizza" />
                    </Box>
                </BoxContainer>
                
                <ExternalOnly>
                    <BoxContainer>
                        <Box>
                            <LocalInfographic
                                linkTo="/cv"
                                content="CV"
                                image={CVImg}
                                imageAlt="cv" />
                        </Box>

                        <Box>
                            <ExternalInfographic
                                linkTo="https://github.com/soltys/"
                                content="Soltys Github"
                                image={GithubImg}
                                imageAlt="github logo"
                            />
                        </Box>

                        <Box>
                            <ExternalInfographic
                                linkTo="https://blog.soltysiak.it"
                                content="Blog"
                                image={BlogImg}
                                imageAlt="lightbulb"
                            />
                        </Box>
                    </BoxContainer>
                </ExternalOnly>
            </div>
        </ViewContainer>
    );
}

export default HomeView;
