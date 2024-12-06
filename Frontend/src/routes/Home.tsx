import {FC} from "react";
import PageContent from "../components/page/PageContent.tsx";
import {Link} from "react-router-dom";

const Home: FC = () => {
    return (
        <PageContent className="min-h-screen">
            <h1 className="text-2xl">Home</h1>
            <br/>
            <Link to="c">Chat</Link>
        </PageContent>
    );
}

export default Home;