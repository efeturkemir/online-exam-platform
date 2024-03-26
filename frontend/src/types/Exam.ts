export interface Exam {
    id: string,
    examType: string,
    examName: string,
    participants: string[],
    questionCount: number,
    questions: Question[]
}

export interface Question {
   questionText: string,
   options: string[],
   answerIndex: number,
}
