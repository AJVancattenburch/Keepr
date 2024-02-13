import { AppState } from '../AppState'
import { Account, Profile } from '../models/Account.js'
import { logger } from '../utils/Logger'
import Pop from "../utils/Pop.js"
import { api } from './AxiosService'

class AccountService {
  async getAccount() {
    try {
      const res = await api.get('/account')
      AppState.account = new Account(res.data)
    } catch (err) {
      logger.error('HAVE YOU STARTED YOUR SERVER YET???', err)
    }
  }

  async updateAccount(accountData) {
    const res = await api.put('/account', { ...accountData} )
    logger.log(res.data, 'Account Updated')
    AppState.account = new Account(res.data)
    Pop.toast(`${AppState.account.name}'s account has been updated!`, 'success', 'top-right', 3000)
  }
}

export const accountService = new AccountService()
