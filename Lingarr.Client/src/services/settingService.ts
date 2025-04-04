﻿import { AxiosError, AxiosResponse, AxiosStatic } from 'axios'
import { ISettingService, ISettings } from '@/ts'

const service = (http: AxiosStatic, resource = '/api/setting'): ISettingService => ({
    getSetting<T>(key: string): Promise<T> {
        return new Promise((resolve, reject) => {
            http.get(`${resource}/${key}`)
                .then((response: AxiosResponse<T>) => {
                    resolve(response.data)
                })
                .catch((error: AxiosError) => {
                    reject(error.response)
                })
        })
    },
    getSettings<T>(keys: string[]): Promise<T> {
        return new Promise((resolve, reject) => {
            http.post(`${resource}/multiple/get`, keys)
                .then((response: AxiosResponse<T>) => {
                    resolve(response.data)
                })
                .catch((error: AxiosError) => {
                    reject(error.response)
                })
        })
    },
    setSetting(key: string, value: string): Promise<void> {
        return new Promise<void>((resolve, reject) => {
            http.post(resource, {
                Key: key,
                Value: value
            })
                .then(() => {
                    resolve()
                })
                .catch((error: AxiosError) => {
                    reject(error.response)
                })
        })
    },
    setSettings(settings: ISettings): Promise<void> {
        return new Promise<void>((resolve, reject) => {
            http.post(`${resource}/multiple/set`, settings)
                .then(() => {
                    resolve()
                })
                .catch((error: AxiosError) => {
                    reject(error.response)
                })
        })
    }
})

export const settingService = (axios: AxiosStatic): ISettingService => {
    return service(axios)
}
