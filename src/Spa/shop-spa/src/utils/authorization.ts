import axios from "axios";
import { IDENTITY_SERVER } from "../configuration/url";

interface IUser {
    name: string,
    role: string
}

export class Authorization {
    async getDetails() : Promise<IUser | null> {
        if (this.isAuthorized() === false) {
            return null;
        }

        let accessToken = localStorage.getItem('access_token');
        var result = await axios.get(`${IDENTITY_SERVER}/connect/userinfo`, {
            headers: {
              'Authorization': `Bearer ${accessToken}` 
            }
        });
        
        return result.data;
    }

    isAuthorized() : boolean {
        let accessToken = localStorage.getItem('access_token');
        if (accessToken === null) {
            return false;
        }

        return true;
    }

    loginUrl() : string {
        return `${IDENTITY_SERVER}/Account/Login?ReturnUrl=%2Fconnect%2Fauthorize%2Fcallback%3Fclient_id%3Dspa%26redirect_uri%3Dhttp%253A%252F%252Flocalhost%253A3000%252Fsignin-oidc%26response_type%3Dtoken%2520id_token%26scope%3Dopenid%2520profile%2520spa%2520basket%2520notifications%2520categories%2520products%2520images%2520orders%2520roles%26response_mode%3Dfragment%26nonce=12345567890`;
    }
}