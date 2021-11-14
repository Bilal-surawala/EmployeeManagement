import { ElMessage } from 'element-plus'

// define a mixin object
export const showMessage = {
    methods: {
        //type:  success/warning/info/error
        showMessage(message, type) {
            ElMessage({ duration: 4000, showClose: true, message: message, type: type })
        }
    }
}