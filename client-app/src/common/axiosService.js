import axios from 'axios';

const instance = axios.create({
    baseURL: 'http://localhost:10299/'
});

// Add a response interceptor
instance.interceptors.response.use(function (response) {
    // Any status code that lie within the range of 2xx cause this function to trigger
    // Do something with response data
    return { success: true, data: response.data }
}, function (error) {
    // Any status codes that falls outside the range of 2xx cause this function to trigger
    // Do something with response error
    let msg = "The request was made and the server responded with Error";
    if (error.message) {
        msg = error.message
    }
    return { success: false, msg: msg }
});

const apiService = {
    async get(url, params) {
        return await instance.get(url, { params });
    },
    async post(url, payload) {
        return await instance.post(url, payload);
    },
    async put(url, payload) {
        return await instance.put(url, payload);
    },
    async delete(url) {
        return await instance.delete(url);
    }
}

export default apiService;