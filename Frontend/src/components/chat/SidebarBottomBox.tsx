import {FC} from "react";
import {useAppSelector} from "../../hooks.ts";
import {UserClient} from "../../util/clients/userClient.ts";
import {useQueryClient} from "react-query";

const SidebarBottomBox: FC = () => {
    const auth = useAppSelector(store => store.auth)
    const queryClient = useQueryClient()
    
    const onLogoutClick = async () => {
        const userClient = new UserClient();
        const logoutRes = await userClient.logout();
        if (logoutRes.ok) {
            await queryClient.invalidateQueries(['auth']);
        }
    } 
    
    return auth.status === "loggedIn"
        ? (
            <div className="grid grid-cols-[1fr_auto] gap-1 m-1">
                <div>
                    <p>{auth.user!.username}</p>
                    <button
                        onClick={onLogoutClick}
                        className="rounded-sm bg-red-900 hover:bg-red-900 hover:font-bold w-full">
                        Logout
                    </button>
                </div>
                <img
                    className="aspect-square w-12 rounded-sm"
                    src={auth.user!.avatarUrl}
                    alt="profile" />
            </div>
        )
        : null;
}

export default SidebarBottomBox;