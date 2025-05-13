import type { RouteObject } from "react-router-dom";
import HomeLayout from "../../layouts/HomeLayout";
import HomePage from "./HomePage";

const HomeRouting = (): RouteObject => ({
    path: '/',
    Component: HomeLayout,
    children: [
        {
            index: true,
            Component: HomePage,
        },
    ],
});

export default HomeRouting;