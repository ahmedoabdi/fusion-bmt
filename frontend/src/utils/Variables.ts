import { useCurrentUser } from '@equinor/fusion'
import { Answer, Barrier } from '../api/models'

export const useAzureUniqueId = (): string => {
    const user = useCurrentUser()
    const azureUniqueId: string = user?.id as string
    return azureUniqueId
}

export enum SavingState {
    Saving,
    Saved,
    NotSaved,
    None,
}

export interface AnswersWithBarrier {
    barrier: Barrier
    answers: (Answer | null)[]
}

export const genericErrorMessage = 'Please try again, or contact systems administrator if the problem persists.'
