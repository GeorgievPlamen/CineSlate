import axios from "axios";
import { CINESLATE_API_URL } from "../config";

axios.defaults.baseURL = CINESLATE_API_URL;
axios.defaults.headers.post['Content-Type'] = 'application/json';

axios.interceptors.request.use(config => {
    const token = `Bearer ${sessionStorage.getItem('JWT')}`;
    config.headers.set('Authorization', token)
    return config;
});

axios.interceptors.response.use(
    response =>  response.data,
    error => {
    const problemDetails: ProblemDetails = {
        status: {code: error.status, text: error.response.statusText},
        title: error.response.data.title,
        traceId: error.response.data.traceId,
        type: error.response.data.type,
        detail: error.response.data.detail
    }

    return Promise.reject(problemDetails);
})

async function login(formData: FormData) {
    try {
        return (await axios.post('users/login',formData))
    } catch (error) {
        console.log(error)
    }
}

async function movies() {
    try {
        return await axios.get('movies');
    } catch (error) {
        console.log(error)
    }
}

interface ProblemDetails {
    status: HttpStatusCode,
    title: string,
    traceId: string,
    type: string,
    detail?: string,
}

interface HttpStatusCode {
    code: number,
    text: string
}


export {login, movies}