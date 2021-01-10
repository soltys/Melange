import { ExternalOnly } from './basic/Basic';
import CVView from './cv/CVView';
import HomeView from './home/HomeView';
import CoffeeView from './coffee/CoffeeView';
import CoffeeMemeView from './coffee/CoffeeMemeView';
import PizzaView from './pizza/PizzaView';
import FastFoodView from './fast-food/FastFoodView';
import {
    BrowserRouter as Router,
    useLocation,
    Switch,
    Route,
    Link
} from "react-router-dom";
import './App.css';

import { useEffect } from "react";


const ScrollToTop = () => {
    const { pathname } = useLocation();

    useEffect(() => {
        window.scrollTo(0, 0);
    }, [pathname]);

    return null;
}

const App = () => {
    return (
        <div className="App">
            <Router>
                <ScrollToTop />
                <div id="navigation-container">
                    <ul className="navigation">
                        <li>
                            <Link to="/"><span role="img" aria-label="home">🏠</span></Link>
                        </li>

                        <li>
                            <Link to="/coffee">Kawa</Link>
                        </li>
                        <li>
                            <Link to="/pizza">Pizza</Link>
                        </li>
                        <li>
                            <Link to="/fast-food">Fast Food</Link>
                        </li>
                        <ExternalOnly>
                            <li>
                                <Link to="/cv">CV</Link>
                            </li>
                        </ExternalOnly>
                    </ul>
                    <hr />
                    <Switch>
                        <Route exact path="/">
                            <HomeView />
                        </Route>
                        <Route path="/cv">
                            <CVView />
                        </Route>
                        <Route path="/coffee">
                            <CoffeeView />
                        </Route>
                        <Route path="/coffee-meme">
                            <CoffeeMemeView />
                        </Route>
                        <Route path="/pizza">
                            <PizzaView />
                        </Route>
                        <Route path="/fast-food">
                            <FastFoodView />
                        </Route>
                    </Switch>
                </div>
            </Router>

        </div>
    );
}

export default App;
