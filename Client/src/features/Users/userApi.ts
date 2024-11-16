import { cineslateApi } from "../../app/api/cineslateApi";

const userApi = cineslateApi.injectEndpoints({
    endpoints: build => ({
        login: build.mutation({
        query: () => 'login'
        })
    })
})

export const {useLoginMutation} = userApi;